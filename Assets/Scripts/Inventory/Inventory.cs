using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemInfo> items = new List<ItemInfo>();

    public void AddItem(ItemInfo item)
    {
        items.Add(item);
    }

    public void RemoveItem(ItemInfo item)
    {
        items.Remove(item);
    }

    public bool ContainsItem(ItemInfo item)
    {
        return items.Contains(item);
    }
    public ItemInfo FindItemByName(string itemName)
    {
        return items.Find(other => itemName == other.Name);
    }

    public IEnumerable<ItemInfo> GetItems()
    {
        return items;
    }

    public List<ItemInfo> GetAllItems()
    {
        return items;
    }
}
