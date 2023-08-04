using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float accelSpeed;
    [SerializeField] private float maxForce;
    
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashDistance = 3;
    
    [SerializeField] private float speed = 8;
    [SerializeField] private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PlayerInput inputs;
    
    private Vector2 targetVelocity;
    private Vector2 lastDirection = Vector2.up;
    private bool isDashing;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
        
    }
    private void LateUpdate()
    {
        ClampPos();
    }

    
    private void Move()
    {
        if (isDashing) return;
        var move = inputs.actions["Movement"].ReadValue<Vector2>().normalized;
        if (move != Vector2.zero) lastDirection = move;
        rb.velocity = move * speed;
        

        Vector2 tVelNormal = targetVelocity.normalized;

        float velDot = Vector2.Dot(move, tVelNormal);

        Vector2 direction = move * (speed * Mathf.Clamp(move.magnitude, 0, 1));

        targetVelocity = Vector2.MoveTowards(targetVelocity, direction,
            accelSpeed * Time.fixedDeltaTime);

        Vector2 force = (targetVelocity - rb.velocity) / Time.fixedDeltaTime;

        force = Vector2.ClampMagnitude(force, maxForce);

        rb.AddForce(force * rb.mass);
    }

    
    public void Dash()
    {
        if (isDashing) return;
        rb.velocity = Vector3.zero;
        isDashing = true;

        float dur = dashDuration;

        DOTween.To(() => dur, x => dur = x, 0, dashDuration).OnComplete(()=>isDashing = false);
        
        var direction = inputs.actions["Movement"].ReadValue<Vector2>().normalized;
        if (direction == Vector2.zero) direction = lastDirection.normalized;
        

        float dashForce = dashDistance / dashDuration;

        var force = direction * dashForce * rb.mass;
        rb.AddForce(force,ForceMode2D.Impulse);

    }
        
        
    private void ClampPos()
    {
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x,- screenBounds.x + transform.localScale.x/2,screenBounds.x - transform.localScale.x/2);
        pos.y = Mathf.Clamp(pos.y,- screenBounds.y +transform.localScale.x/2,screenBounds.y - transform.localScale.x/2);
        transform.position = pos;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Station>()) return;
        Debug.Log("collide");
        var station = col.GetComponent<Station>();
        if (!station.HasCube) return;
        sr.color = station.Color;
        station.RemoveCube();
    }
}
