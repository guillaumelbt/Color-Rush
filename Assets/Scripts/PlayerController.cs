using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerData data;
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
    private float lifeTime;

    private bool dashInCd = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        InitData();
    }

    void InitData()
    {
        accelSpeed = data.accelerationSpeed;
        maxForce = data.maximumForce;
        speed = data.maxSpeed;
        dashDistance = data.dashDistance;
        dashDuration = data.dashDuration;
    }

    private void Update()
    {
#if UNITY_EDITOR
        InitData();
#endif
    }

    private void FixedUpdate()
    {
        Move();
        
    }
    private void LateUpdate()
    {
        ClampPos();
    }

    [SerializeField] private GameObject menu;
    public void Menu(CallbackContext ctx)
    {
        if (!ctx.started) return;
        if (GameManager.instance.Life == 0) return;
        menu.SetActive(!menu.activeSelf);
        Time.timeScale = menu.activeSelf ? 0 : 1;
    }

    private void Move()
    {
        if (isDashing) return;

        var move = inputs.actions["Movement"].ReadValue<Vector2>().normalized;
        if (move != Vector2.zero) lastDirection = move;

        animator.SetBool("moving", move != Vector2.zero);
        
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
        if (isDashing || dashInCd) return;
        rb.velocity = Vector3.zero;
        isDashing = true;
        dashInCd = true;
        float dur = dashDuration;
        float dur2 = data.dashCooldown;


        

        DOTween.To(() => dur, x => dur = x, 0, dashDuration).OnComplete(() => isDashing = false);
        
        DOTween.To(() => dur2, x => dur2 = x, 0, data.dashCooldown).OnComplete(()=> 
        {
            dashInCd = false;
            
        }
        );
        
        var direction = inputs.actions["Movement"].ReadValue<Vector2>().normalized;
        if (direction == Vector2.zero) direction = lastDirection.normalized;

        

        float dashForce = dashDistance / dashDuration;

        var force = direction * dashForce * rb.mass;
        rb.AddForce(force,ForceMode2D.Impulse);

        animator.SetFloat("Direction", Mathf.Abs(Vector3.Dot(force,Vector3.up)));
        animator.ResetTrigger("Dash");
        animator.SetTrigger("Dash");

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
        if (sr.color == Color.white)
        {
            if (col.GetComponent<Station>())
            {
                var station = col.GetComponent<Station>();
                if (!station.HasCube) return;
                score = GameManager.instance.CalculateScore(station.CurrentCube);
                sr.color = station.Color;
                station.RemoveCube();
            }
        }
        else
        {
            if (col.GetComponent<Generator>())
            {
                col.GetComponent<Generator>().Score(sr.color);
                sr.color = Color.white;
                GameManager.instance.score += score;
                Debug.Log(GameManager.instance.score);
            }
        }
    }

    private int score = 0;
    
}
