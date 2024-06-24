using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] Transform _acquirer;
    public bool Acquirable = false;
    private Item _item;
    private void OnEnable()
    {
        _item = GetComponent<ItemMono>().Item;
    }
    private void OnDisable()
    {
        _item = null;
    }

    public void SetAcquirer(Transform acquirer)
    {
        _acquirer = acquirer;
    }

    public Transform GetAcquirer()
    {
        return _acquirer;
    }

    public Item GetItem()
    {
        return _item;
    }

    public void Init(Item item)
    {
        _item = item;
    }

    public void TriggerAcquirable()
    {
        Acquirable = true;
    }
}