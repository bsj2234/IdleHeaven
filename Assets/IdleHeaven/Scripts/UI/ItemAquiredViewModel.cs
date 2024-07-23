using IdleHeaven;
using System;
using UnityEngine;

public class ItemAquiredViewModel : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemAcquirer _itemAcquirer;

    public Action<Item> OnItemAquirePopup;

    public void Init()
    {
        _itemAcquirer.OnItemAcquired += OnItemGet;
    }

    private void OnItemGet(Item item)
    {
        OnItemAquirePopup?.Invoke(item);
    }
}
