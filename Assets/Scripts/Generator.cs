using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private SpriteRenderer sr;
    private Color baseColor;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;

    }
    public void Score(Color color)
    {
        animator.ResetTrigger("Score");
        animator.SetTrigger("Score");
        sr.DOColor(color, 0.5f).OnComplete(()=> sr.DOColor(baseColor, 0.5f));
    }
}
