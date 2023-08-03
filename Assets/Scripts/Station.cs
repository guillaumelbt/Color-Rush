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
    
    [Range(0,1)]
    [SerializeField] private float probability = 0.2f;
    [SerializeField] private float cubeLifeTime = 5f;
    [SerializeField] private float cooldown = 15f;
    
    private StationState state = StationState.Search;
    private float elapsedTime = 0;

    private GameObject cube;
    private void Update()
    {
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
        Debug.Log(rnd);
        if (rnd <= probability)
        {
            Debug.Log("Success");
            state = StationState.Cube;
            cube = Pooler.instance.Pop("Cube");
            cube.transform.position = transform.position;
        }
    }

    private void Cube()
    {
        if (!(Time.time - elapsedTime > cubeLifeTime)) return;
        elapsedTime = Time.time;
        state = StationState.Cooldown;
        Pooler.instance.Depop("Cube",cube);
        cube = null;
    }

    private void Cooldown()
    {
        if (!(Time.time - elapsedTime > cooldown)) return;
        elapsedTime = Time.time;
        state = StationState.Search;
    }
}
