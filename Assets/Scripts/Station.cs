using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private float cubeLifeTime = 5f;
    [SerializeField] private float cooldown = 15f;
    
    [SerializeField] private Crown crown = Crown.One;
    
    private StationState state = StationState.Search;
    private float elapsedTime = 0;

    private Cube cube;

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
                cubeLifeTime = GameManager.instance.data.cubeLifeTimeInStation.x;
                cooldown = GameManager.instance.data.cooldown.x;
                ;
                break;
            case Crown.Two:
                probability = GameManager.instance.data.probability.y;
                cubeLifeTime = GameManager.instance.data.cubeLifeTimeInStation.y;
                cooldown = GameManager.instance.data.cooldown.y;
                break;
            case Crown.Three:
                probability = GameManager.instance.data.probability.z;
                cubeLifeTime = GameManager.instance.data.cubeLifeTimeInStation.z;
                cooldown = GameManager.instance.data.cooldown.z;
                break;
        }
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
        if (!(Time.time - elapsedTime > 1)) return;
        elapsedTime = Time.time;
        float rnd = Random.Range(0f, 1f);
        if (rnd <= probability)
        {
            state = StationState.Cube;
            GameObject go = Pooler.instance.Pop("Cube");
            cube = go.GetComponent<Cube>();
            cube.transform.position = transform.position;
        }
    }

    private void Cube()
    {
        if (!(Time.time - elapsedTime > cubeLifeTime)) return;
        RemoveCube();
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
    }
}
