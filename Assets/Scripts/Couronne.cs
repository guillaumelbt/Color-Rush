using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Couronne : MonoBehaviour
{
    [SerializeField] private float radius = 1;
    [SerializeField] private Color color = Color.red;
    private void OnDrawGizmosSelected()
    {
        Handles.color = color;
        Handles.DrawWireDisc(Vector3.zero,Vector3.forward,radius);
    }
}
