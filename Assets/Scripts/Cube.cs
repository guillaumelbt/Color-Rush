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
        if(GameManager.instance.data.colors.Length < 1 )
            color = Random.ColorHSV();
        else
        {
            var rnd = Random.Range(0, GameManager.instance.data.colors.Length);
            color = GameManager.instance.data.colors[rnd];
        }
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;
    }

}
