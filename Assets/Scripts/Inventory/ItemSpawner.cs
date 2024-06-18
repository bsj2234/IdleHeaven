using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdleHeaven;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public ItemGenerator generator;
    public DroppedItem SpawnItem(Transform position, GameObject itemPrefab)
    {
        Instantiate(itemPrefab, position.position, position.rotation)
            .TryGetComponent(out DroppedItem itemObj);
        return itemObj;
    }
}
