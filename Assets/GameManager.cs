using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    private int lifeLeft;
    public int cubeOnScreen;
    [SerializeField] private TMP_Text scoreText;
    public bool CanGenerateCube => cubeOnScreen < data.maxCubeOnScreen;
    
    public int score = 0;
    private float coef;

    public int CalculateScore(Cube cube)
    {
        return Mathf.RoundToInt(data.point * data.pointCurve.Evaluate(cube.LifeTime) * coef);
    }

    private void Update()
    {
        scoreText.text = $"Score : {score}";

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
