using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public enum EquipmentType { Head, Chest, Legs, Weapon, Shield , None}

    public class Equipments : MonoBehaviour
    {
        [JsonIgnore]
        [SerializeField] private EquipmentItem[] _slotVisualize; 
        [JsonIgnore]
        private Dictionary<EquipmentType, EquipmentItem> _equippedItems = new Dictionary<EquipmentType, EquipmentItem>();

        public List<EquipmentItem> EquippedItems => new List<EquipmentItem>(_equippedItems.Values);

        [JsonIgnore]
        public Action<EquipmentType, Item, Equipments> OnEquipmentsChagned { get; set; }

        public event Action<Equipments, EquipmentType, EquipmentItem> OnEquipped;
        public event Action<Equipments, EquipmentType, EquipmentItem> OnUnEquipped;


        [JsonIgnore]
        private Stats _itemStats = new Stats();

        private void Awake()
        {
            _slotVisualize = new EquipmentItem[Enum.GetValues(typeof(EquipmentType)).Length];
        }



        // Equip an item to a itemDatas slot
        public EquipmentItem Equip(EquipmentItem item)
        {
            EquipmentType slot = item.EquipmentData.EquipmentSlot;
            return Equip(slot, item);
        }

        public EquipmentItem Equip(EquipmentType slot, EquipmentItem item)
        {
            EquipmentItem previousItem = null;

            // If there is already an item equipped in the slot, remove it
            if (_equippedItems.ContainsKey(slot))
            {
                previousItem = Unequip(slot);
                _equippedItems[slot] = item;
            }
            else
            {
                _equippedItems.Add(slot, item);
            }
            _equippedItems[slot].Equiped = true;
            OnEquipped?.Invoke(this, slot, item);
            CalcEquipmentsStat();
            OnEquipmentsChagned?.Invoke(slot, item, this);

            item.OnItemChanged += OnItemChanged;

            // Update the visual representation of the equipment
            var vals = Enum.GetValues(typeof(EquipmentType));
            for (int i = 0; i < vals.Length; i++)
            {
                if (_equippedItems.ContainsKey((EquipmentType)i))
                {
                    _slotVisualize[i] = item;
                }
                else
                {
                    _slotVisualize[i] = null;
                }
            }

            return previousItem;
        }

        public EquipmentItem Unequip(EquipmentType slot)
        {
            EquipmentItem previousItem = null;
            if (_equippedItems.ContainsKey(slot))
            {
                previousItem = _equippedItems[slot];
                _equippedItems[slot].Equiped = false;
                _equippedItems.Remove(slot);
                OnUnEquipped?.Invoke(this, slot, previousItem);
            }
            OnEquipmentsChagned?.Invoke(slot, previousItem, this); 
            CalcEquipmentsStat();

            previousItem.OnItemChanged -= OnItemChanged;
            return previousItem;
        }

        public EquipmentItem GetEquippedItem(EquipmentType slot)
        {
            _equippedItems.TryGetValue(slot, out var item);
            return item;
        }

        public Dictionary<EquipmentType, EquipmentItem> GetEquippedItems()
        {
            return new Dictionary<EquipmentType, EquipmentItem>(_equippedItems);
        }

        public bool CanEquip(EquipmentType slot, EquipmentItem item)
        {
            // Add your own validation logic here
            return true;
        }

        public void ClearAllEquipment()
        {
            var equippedItemsCopy = new Dictionary<EquipmentType, EquipmentItem>(_equippedItems);
            _equippedItems.Clear();
            foreach (var kvp in equippedItemsCopy)
            {
                OnUnEquipped?.Invoke(this, kvp.Key, kvp.Value);
            }
        }

        private void CalcEquipmentsStat()
        {
            _itemStats.Clear();
            
            foreach (EquipmentItem equipment in _equippedItems.Values)
            {
                _itemStats.AddStats(equipment.ResultStats);
            }
        }
        private void OnItemChanged()
        {
            CalcEquipmentsStat();
            OnEquipmentsChagned?.Invoke(EquipmentType.None, null, this);
        }

        public void LoadEquipments(List<EquipmentItem> equipments)
        {
            
            Dictionary<string, ItemData> itemArmor = CSVParser.Instance.GetItems(ItemType.Armor);
            Dictionary<string, ItemData> itemWeapon = CSVParser.Instance.GetItems(ItemType.Weapon);


            foreach (EquipmentItem item in equipments)
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
                    item.EquipmentData = itemArmor[item.Name] as EquipmentData;
                    Equip(item);
                    item.RefreshRarity();
                    continue;
                }
                else if (itemWeapon.ContainsKey(item.Name))
                {
                    item.ItemData = itemWeapon[item.Name];
                    item.EquipmentData = itemWeapon[item.Name] as EquipmentData;
                    Equip(item);
                    item.RefreshRarity();
                    continue;
                }
                else
                {
                    Debug.LogError("Item not found in item data");
                    continue;
                }
            }
    }

        internal void Clear()
        {
            _equippedItems.Clear();
        }
    }
}
