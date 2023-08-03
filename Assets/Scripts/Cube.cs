using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(SpriteRenderer))]
public class Cube : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color color;
    
    private void Start()
    {
        color = Random.ColorHSV();
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;
    }

}
