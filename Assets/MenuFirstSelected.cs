using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFirstSelected : MonoBehaviour
{
    [SerializeField] private GameObject go;
    

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(go);
    }
}
