using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transflator : MonoBehaviour
{
    [SerializeField] Vector3 Accel;

    private void OnEnable()
    {
        transform.localPosition= Vector3.zero;
    }
    void Update()
    {
        transform.localPosition = transform.localPosition + Accel * Time.deltaTime;
    }
}
