using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        private Dictionary<string ,Item> hashedItem = new Dictionary<string, Item>();

        public List<Item> Items
        {
            get => items;
            set
            {
                items = value;
                OnInventoryChanged?.Invoke(null);
            }
        }

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
            if(item is not EquipmentItem)
            {
                if (hashedItem.ContainsKey(item.Name))
                {
                    Item foundItem = hashedItem[item.Name];
                    foundItem.Quantity += item.Quantity;
                    OnInventoryChanged?.Invoke(foundItem);
                    return;
                }
                else
                {
                    hashedItem.Add(item.Name, item);
                    items.Add(item);
                    OnInventoryChanged?.Invoke(item);
                    return;
                }
            }
            else
            {
                items.Add(item);
                OnInventoryChanged?.Invoke(item);
                return;
            }

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
