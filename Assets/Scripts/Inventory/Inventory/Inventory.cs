using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] List<Item> items;

        public List<Item> Items => items;

        [SerializeField] ItemAcquirer[] acquirers;
        public readonly int INVENTORY_SIZE = 200;


        public event Action<Item> OnInventoryChanged;

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
            OnInventoryChanged?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            OnInventoryChanged?.Invoke(item);
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

        public List<Item> GetItems()
        {
            return items;
        }

        public List<Item> GetAllItems()
        {
            return items;
        }
    }
}
