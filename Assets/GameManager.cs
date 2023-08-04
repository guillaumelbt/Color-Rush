using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private Image timerImage;

    private float timer = 30;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        timerImage.fillAmount = timer / 30;
        timer -= Time.deltaTime;
        if(timer <= 0) Application.Quit();
    }

    public void ChangeTimer(float amount)
    {
        timer += amount;
        timer = Mathf.Clamp(timer, 0, 30);
    }
}
