using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    private int lifeLeft;
    public int cubeOnScreen;
    public bool CanGenerateCube => cubeOnScreen < data.maxCubeOnScreen;
    
    public float score = 0;
    private float coef;

    public float CalculateScore(Cube cube)
    {
        return data.point * data.pointCurve.Evaluate(cube.LifeTime) * coef;
    }

    private void Update()
    {
        if (Time.time - elapsedTime > 60)
        {
            elapsedTime = Time.time;
            coef += data.coef;
        }
    }

    public int Life
    {
        get => lifeLeft;
        set => lifeLeft = value;
    }

    private float elapsedTime = 0;
    
    void Start()
    {
        lifeLeft = data.lifeNumber;
        coef = data.coef;

    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
