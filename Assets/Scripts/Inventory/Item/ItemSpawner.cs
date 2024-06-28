using UnityEngine;
using IdleHeaven;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] public ItemGenerator generator;
    [SerializeField] public GameObject droppedItemPrefab;
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
