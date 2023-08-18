using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    void OnEnable()
    {
        text.text = $"{GameManager.instance.score}";
    }


}
