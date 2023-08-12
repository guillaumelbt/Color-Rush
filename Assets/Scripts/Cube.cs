using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(SpriteRenderer))]
public class Cube : MonoBehaviour
{
    private float lifeTime;
    private float elapsedTime = 0;

    public float LifeTime => Time.time - elapsedTime / lifeTime;
    public bool isAlive => Time.time - elapsedTime > lifeTime;
    private SpriteRenderer sr;
    public Color color;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        lifeTime = GameManager.instance.data.cubeLifeTime;
    }
    public void ChangeColor()
    {
        if(GameManager.instance.data.colors.Length < 1 )
            color = Random.ColorHSV();
        else
        {
            var rnd = Random.Range(0, GameManager.instance.data.colors.Length);
            color = GameManager.instance.data.colors[rnd];
        }
        
        sr.color = color;
        elapsedTime = Time.time;
    }
}
