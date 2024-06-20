using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdleHeaven;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public ItemGenerator generator;
    public DroppedItem SpawnItem(Transform position, Item item)
    {
        Instantiate(item.ItemData.ItemPrefab, position.position, position.rotation)
            .TryGetComponent(out DroppedItem itemObj);
        itemObj.GetComponent<ItemMono>().Init(item);
        itemObj.Init(item);
         return itemObj;
    }
}