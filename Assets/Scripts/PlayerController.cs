using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PlayerInput inputs;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Move();
        
    }
    private void LateUpdate()
    {
        ClampPos();
    }

    private void Move()
    {
        var move = inputs.actions["Movement"].ReadValue<Vector2>().normalized;
        rb.velocity = move * speed;
    }

    private void ClampPos()
    {
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x,- screenBounds.x + transform.localScale.x/2,screenBounds.x - transform.localScale.x/2);
        pos.y = Mathf.Clamp(pos.y,- screenBounds.y +transform.localScale.x/2,screenBounds.y - transform.localScale.x/2);
        transform.position = pos;
    }
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.GetComponent<Cube>())
        {
            sr.color = col.transform.GetComponent<Cube>().color;
            Pooler.instance.Depop("Cube",col.gameObject);
        }
    }
}
