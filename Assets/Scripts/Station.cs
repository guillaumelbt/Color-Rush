using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Station : MonoBehaviour
{
    public enum StationState
    {
        Search,
        Cube,
        Cooldown
    }  
    public enum Crown
    {
        One,
        Two,
        Three
    }  
    
    [Range(0,1)]
    [SerializeField] private float probability = 0.2f;
    [SerializeField] private float cooldown = 15f;
    
    [SerializeField] private Crown crown = Crown.One;

    [SerializeField] private Collider2D col;
    private StationState state = StationState.Search;
    private float elapsedTime = 0;

    private Cube cube;

    [CanBeNull] public Cube CurrentCube
    {
        get
        {
            if (cube is not null) return cube;
            return null;
        }
    }

    public Color Color => cube.color;
    public bool HasCube => cube != null;


    private void Start()
    {
        InitData();
    }

    private void InitData()
    {
        switch (crown)
        {
            case Crown.One:
                probability = GameManager.instance.data.probability.x;
                ;
                break;
            case Crown.Two:
                probability = GameManager.instance.data.probability.y;
                break;
            case Crown.Three:
                probability = GameManager.instance.data.probability.z;
                break;
        }
        cooldown = GameManager.instance.data.cooldown;
    }

    private void Update()
    {
#if UNITY_EDITOR
        InitData();
#endif
        
        switch (state)
        {
            case StationState.Search :
                Search();
                break;
            case StationState.Cube :
                Cube();
                break;
            case StationState.Cooldown :
                Cooldown();
                break;
        }

        
    }

    private void Search()
    {
        if (!GameManager.instance.CanGenerateCube) return;
        if (!(Time.time - elapsedTime > 1)) return;
        elapsedTime = Time.time;
        float rnd = Random.Range(0f, 1f);
        if (rnd <= probability)
        {
            state = StationState.Cube;
            GameObject go = Pooler.instance.Pop("Cube");
            cube = go.GetComponent<Cube>();
            cube.ChangeColor();
            cube.transform.position = transform.position;
            col.gameObject.SetActive(true); 
        }
    }

    private void Cube()
    {
        if (!(cube.isAlive)) return;
        RemoveCube();
        GameManager.instance.Life--;
        if (GameManager.instance.Life == 0)
        {
            Application.Quit();
        }
    }

    private void Cooldown()
    {
        if (!(Time.time - elapsedTime > cooldown)) return;
        elapsedTime = Time.time;
        state = StationState.Search;
    }

    public void RemoveCube()
    {
        elapsedTime = Time.time;
        state = StationState.Cooldown;
        Pooler.instance.Depop("Cube",cube.gameObject);
        cube = null;
        col.gameObject.SetActive(false); 
    }
}
