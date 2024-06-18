using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public DroppedItem SpawnItem(Transform position, GameObject itemPrefab)
    {
        Instantiate(itemPrefab, position.position, position.rotation)
            .TryGetComponent(out DroppedItem itemObj);
        return itemObj;
    }
}
