using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [Header("General Data")]
    public int lifeNumber = 3;
    public int maxCubeOnScreen = 3;
    public Difficulty[] difficulties;
    
    [Header("Score")] 
    public float coef = 1.5f;
    public float point = 100;
    public AnimationCurve pointCurve;
    
    [Header("Station")]
    public float cooldown = 15;

    [Header("Cube")]
    public Color[] colors;
    public float cubeLifeTime = 10;
}
