using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        private Dictionary<string, Item> hashedItem = new Dictionary<string, Item>();

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
            if (item is not EquipmentItem)
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
                    item.CurrentIndex = items.Count - 1;
                    OnInventoryChanged?.Invoke(item);
                    return;
                }
            }
            else
            {
                items.Add(item);
                item.CurrentIndex = items.Count - 1;
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

        internal void LoadItems(List<Item> items)
        {
            Dictionary<string, ItemData> itemUsable = CSVParser.Instance.GetItems(ItemType.Usable);


            foreach (Item item in items)
            {
                if (item.Name == null)
                {
                    Debug.Log("Item name is null");
                    continue;
                }
                else
                {
                    Debug.Log(item.Name);
                }

                if (itemUsable.ContainsKey(item.Name))
                {
                    item.ItemData = itemUsable[item.Name];
                    AddItem(item);
                    continue;
                }
                else
                {
                    Debug.LogError("Item not found in LoadInventory");
                }
            }
        }

        internal void LoadEquipmentItems(List<EquipmentItem> equipmentItems)
        {
            Dictionary<string, ItemData> itemArmor = CSVParser.Instance.GetItems(ItemType.Armor);
            Dictionary<string, ItemData> itemWeapon = CSVParser.Instance.GetItems(ItemType.Weapon);


            foreach (EquipmentItem item in equipmentItems)
            {
                if (item.Name == null)
                {
                    Debug.Log("Item name is null");
                    continue;
                }
                else
                {
                    Debug.Log(item.Name);
                }

                if (itemArmor.ContainsKey(item.Name))
                {
                    item.ItemData = itemArmor[item.Name];
                    AddItem(item);
                    item.RefreshRarity();
                    continue;
                }
                else if (itemWeapon.ContainsKey(item.Name))
                {
                    item.ItemData = itemWeapon[item.Name];
                    AddItem(item);
                    item.RefreshRarity();
                    continue;
                }
                else
                {
                    Debug.LogError("Item not found in LoadInventory");
                }
            }
        }

        internal void Clear()
        {
            items.Clear();
            hashedItem.Clear();
        }
    }
}
