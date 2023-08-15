using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float maxSpeed = 6;
    public float maximumForce = 200;
    public float accelerationSpeed = 150;

    public float dashDuration = 0.2f;
    public float dashDistance = 3f;
}
