using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Difficulty : ScriptableObject
{
    public int duration = 60;
    [Range(0,1)] public float crown1Probability = 0.2f;
    [Range(0,1)] public float crown2Probability = 0.2f;
    [Range(0,1)] public float crown3Probability = 0.2f;
}
