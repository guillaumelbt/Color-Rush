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
    private SpriteRenderer sr;
    public float LifeTime => (Time.time - elapsedTime) / lifeTime;
    public bool isAlive => Time.time - elapsedTime > lifeTime;
    public Color color;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        lifeTime = GameManager.instance.data.cubeLifeTime;
    }

    private void OnEnable()
    {
        elapsedTime = Time.time;
        sr.material.SetFloat("_DissolveValue", 1f);
    }


    void Update()
    {
        sr.material.SetFloat("_DissolveValue", 1 - LifeTime);
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
        sr.material.SetColor("_Color",color);
        
    }
}
