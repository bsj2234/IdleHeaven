using IdleHeaven;
using System;
using UnityEngine;

public class ItemAcquirer : MonoBehaviour
{
    public Action<Item> OnItemAcquired;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Test Trigger {other.gameObject.name}");
        //뭘로 판단할까
        if (other.TryGetComponent(out DroppedItem droppedItem))
        {
            Acquire(droppedItem);
        }
    }

    private void Acquire(DroppedItem droppedItem)
    {
        if (droppedItem.GetAcquirer() == transform.parent)
        {
            Debug.Log("ItemAcquired");
            if (OnItemAcquired != null)
            {
                OnItemAcquired.Invoke(droppedItem.GetItem());
            }
            Destroy(droppedItem.gameObject);
        }
    }
}
