using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] Transform _acquirer;

    public void SetAcquirer(Transform acquirer)
    {
        _acquirer = acquirer;
    }

    public Transform GetAcquirer()
    {
        return _acquirer;
    }
}
