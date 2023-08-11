using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    [SerializeField] private Image timerImage;

    private float timer = 30;
    private int lifeLeft;

    void Start()
    {
        timer = data.timer;
        lifeLeft = data.lifeNumber;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        timerImage.fillAmount = timer / data.timer;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            lifeLeft--;
            if(lifeLeft < 0) Application.Quit();
        }
    }

    public void ChangeTimer(float amount)
    {
        timer += amount;
        timer = Mathf.Clamp(timer, 0, data.timer);
    }
}
