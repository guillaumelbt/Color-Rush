using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(Vector3.one*2, 1).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
