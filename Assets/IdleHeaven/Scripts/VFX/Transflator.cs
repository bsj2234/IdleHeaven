using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transflator : MonoBehaviour
{
    [SerializeField] Vector3 Accel;
    [SerializeField] bool Ratio;
    [SerializeField] float original;
    [SerializeField] float dest;
    float ratio = 1f;
    private void OnEnable()
    {
        transform.localPosition= Vector3.zero;
        dest = Screen.height;
        if(Ratio)
        {
            ratio = dest / original;
        }
    }
    void Update()
    {
        transform.localPosition = transform.localPosition + Accel * ratio * Time.deltaTime;
    }
}
