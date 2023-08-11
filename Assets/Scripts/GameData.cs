using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [Header("General Data")] 
    public float timer = 30;
    public int lifeNumber = 3;
    public float cubeBonus = 5;
    public float cubeMalus = 3;
    
    [Header("Player")] 
    public float maxSpeed = 6;
    public float maximumForce = 200;
    public float accelerationSpeed = 150;

    public float dashDuration = 0.2f;
    public float dashDistance = 3f;
    
    [Header("Station")] 
    public Vector3 probability = new Vector3(0.2f,0.2f,0.2f);
    public Vector3 cubeLifeTimeInStation = new Vector3(5,5,5);
    public Vector3 cooldown = new Vector3(15,15,15);

    [Header("Cube")] 
    public Color[] colors;
    public float cubeLifeTimePickedUp = 5f;
}
