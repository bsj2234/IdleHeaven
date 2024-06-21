using IdleHeaven;
using System;
using UnityEngine;

public class ItemAcquirer : MonoBehaviour
{
    public Action<Item> OnItemAcquired;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DroppedItem droppedItem))
        {
            Acquire(droppedItem);
        }
    }

    private void Acquire(DroppedItem droppedItem)
    {
        if (droppedItem.GetAcquirer() == transform.parent)
        {
            if (OnItemAcquired != null)
            {
                OnItemAcquired.Invoke(droppedItem.GetItem());
            }
            Destroy(droppedItem.gameObject);
        }
    }
}
