using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [Header("General Data")]
    public int lifeNumber = 3;
    public int maxCubeOnScreen = 3;

    [Header("Score")] 
    public float coef = 1.5f;
    public float point = 100;
    public AnimationCurve pointCurve;
    
    [Header("Player")] 
    public float maxSpeed = 6;
    public float maximumForce = 200;
    public float accelerationSpeed = 150;

    public float dashDuration = 0.2f;
    public float dashDistance = 3f;
    
    [Header("Station")] 
    public Vector3 probability = new Vector3(0.2f,0.2f,0.2f);
    public float cooldown = 15;

    [Header("Cube")]
    public Color[] colors;
    public float cubeLifeTime = 10;
}
