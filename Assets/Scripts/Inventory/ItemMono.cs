using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMono : MonoBehaviour
{
    [field: SerializeField]
    public Item Item { get; set; }

    public void Init(Item item)
    {
        Item = item;
    }
}
