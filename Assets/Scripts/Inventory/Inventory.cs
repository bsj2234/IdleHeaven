using System.Collections.Generic;
using UnityEngine;
using IdleHeaven;
using System.Linq;
public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;

    [SerializeField] ItemAcquirer[] acquirers;

    private void Awake()
    {
        acquirers = GetComponentsInChildren<ItemAcquirer>();
        foreach (ItemAcquirer acquirer in acquirers)
        {
            acquirer.OnItemAcquired += HandleItemAcquired;
        }

    }

    private void HandleItemAcquired(Item item)
    {
        AddItem(item);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
    public Item FindItemByName(string itemName)
    {
        return items.Find(other => itemName == other.Name);
    }

    public IEnumerable<Item> GetItems()
    {
        return items;
    }

    public List<Item> GetAllItems()
    {
        return items;
    }
}
