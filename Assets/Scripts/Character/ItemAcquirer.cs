using IdleHeaven;
using System;
using UnityEngine;

public class ItemAcquirer : MonoBehaviour
{
    [SerializeField] private CurrencyInventory _currencyInventory;

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
        if (droppedItem.GetAcquirer() == transform.parent && droppedItem.Acquirable)
        {
            if(droppedItem.GetItem() is Currency currency)
            {
                _currencyInventory.AddCurency(currency);
            }


            if (OnItemAcquired != null)
            {
                OnItemAcquired.Invoke(droppedItem.GetItem());
            }
            Destroy(droppedItem.gameObject);
        }
    }
}
