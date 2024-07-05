using UnityEngine;
using IdleHeaven;
using System;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public ItemGenerator generator;
    [SerializeField] public GameObject droppedItemPrefab;

    public void Init(ItemDropTableData itemDrop)
    {
        generator.Init(new RarityTable(itemDrop.Common,itemDrop.Uncommon,itemDrop.Epic,itemDrop.Unique,itemDrop.Legendary));
    }

    public DroppedItem SpawnItem(Transform position, Item item)
    {
        if(item == null || position == null)
        {
            Debug.LogError("Item or position is null");
            return null;
        }
        Instantiate( droppedItemPrefab, position.position, position.rotation)
            .TryGetComponent(out DroppedItem itemObj);
        itemObj.Init(item);

         return itemObj;
    }

}
