using System;
using UnityEngine;
using IdleHeaven;

public class ItemViewModel<T> : IViewModel where T : Item
{
    private T item;

    public string ItemName => item.Name;
    public Sprite ItemIcon => item.ItemData.Icon; // Assuming the prefab has a SpriteRenderer

    public ItemViewModel(T newItem)
    {
        SetItem(newItem);
    }

    public void SetItem(T newItem)
    {
        item = newItem;
        OnItemChanged?.Invoke();
    }

    public void Refresh()
    {
        OnItemChanged?.Invoke();
    }

    public event Action OnItemChanged;
}
