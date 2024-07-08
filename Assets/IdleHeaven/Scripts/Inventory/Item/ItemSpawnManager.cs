using UnityEngine;
using IdleHeaven;
using System;

public class ItemSpawnManager : MonoSingleton<ItemSpawnManager>
{
    [SerializeField] public ItemGenerator generator;
    [SerializeField] public GameObject droppedItemPrefab;

    public void Init(ItemDropTableData itemDrop)
    {
        generator = new ItemGenerator();
        generator.Init(new RarityTable(itemDrop.None, itemDrop.Currency, itemDrop.Common,itemDrop.Uncommon,itemDrop.Epic,itemDrop.Unique,itemDrop.Legendary));
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
        itemObj.GetComponent<ItemMono>().Init(item);

         return itemObj;
    }

}
