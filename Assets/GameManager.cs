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

    public float CalculateScore(Cube cube)
    {
        return data.point * data.pointCurve.Evaluate(cube.LifeTime) * data.coef;
    }

    public int Life
    {
        get => lifeLeft;
        set => lifeLeft = value;
    }

    void Start()
    {
        lifeLeft = data.lifeNumber;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
