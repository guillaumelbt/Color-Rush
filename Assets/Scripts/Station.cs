using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                probability = GameManager.instance.CurrentLevel.crown1Probability;
                break;
            case Crown.Two:
                probability = GameManager.instance.CurrentLevel.crown2Probability;
                break;
            case Crown.Three:
                probability = GameManager.instance.CurrentLevel.crown3Probability;
                break;
        }
        cooldown = GameManager.instance.data.cooldown;
    }

    private void Update()
    {
        InitData();

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
            GameManager.instance.cubeOnScreen++;
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
            GameManager.instance.GameOver();
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
        GameManager.instance.cubeOnScreen--;
        elapsedTime = Time.time;
        state = StationState.Cooldown;
        Pooler.instance.Depop("Cube",cube.gameObject);
        cube = null;
        col.gameObject.SetActive(false); 
    }
}
