using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield }

    public class Equipments : MonoBehaviour
    {
        [SerializeField] private EquipmentItem[] _slotVisualize;
        [SerializeField] private CharacterStats _characterStats;
        private Dictionary<EquipmentSlot, EquipmentItem> _equippedItems = new Dictionary<EquipmentSlot, EquipmentItem>();

        public Action<EquipmentSlot, Item> OnEquipmentsChagned { get; set; }

        public event Action<Equipments, EquipmentSlot, EquipmentItem> OnEquipped;
        public event Action<Equipments, EquipmentSlot, EquipmentItem> OnUnEquipped;

        private void Awake()
        {
            _slotVisualize = new EquipmentItem[Enum.GetValues(typeof(EquipmentSlot)).Length];
        }



        // Equip an item to a itemDatas slot
        public EquipmentItem Equip(EquipmentItem item)
        {
            EquipmentSlot slot = item.ItemData.EquipmentSlot;
            return Equip(slot, item);
        }

        public EquipmentItem Equip(EquipmentSlot slot, EquipmentItem item)
        {
            EquipmentItem previousItem = null;

            // If there is already an item equipped in the slot, remove it
            if (_equippedItems.ContainsKey(slot))
            {
                previousItem = _equippedItems[slot];
                OnUnEquipped?.Invoke(this, slot, previousItem);
                _equippedItems[slot] = item;
            }
            else
            {
                _equippedItems.Add(slot, item);
            }
            OnEquipped?.Invoke(this, slot, item);
            OnEquipmentsChagned?.Invoke(slot, item);

            // Update the visual representation of the equipment
            var vals = Enum.GetValues(typeof(EquipmentSlot));
            for (int i = 0; i < vals.Length; i++)
            {
                if (_equippedItems.ContainsKey((EquipmentSlot)i))
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

        public EquipmentItem Unequip(EquipmentSlot slot)
        {
            EquipmentItem previousItem = null;
            if (_equippedItems.ContainsKey(slot))
            {
                previousItem = _equippedItems[slot];
                _equippedItems.Remove(slot);
                OnUnEquipped?.Invoke(this, slot, previousItem);
            }
            OnEquipmentsChagned?.Invoke(slot, previousItem);
            return previousItem;
        }

        public EquipmentItem GetEquippedItem(EquipmentSlot slot)
        {
            _equippedItems.TryGetValue(slot, out var item);
            return item;
        }

        public Dictionary<EquipmentSlot, EquipmentItem> GetEquippedItems()
        {
            return new Dictionary<EquipmentSlot, EquipmentItem>(_equippedItems);
        }

        public bool CanEquip(EquipmentSlot slot, EquipmentItem item)
        {
            // Add your own validation logic here
            return true;
        }

        public void ClearAllEquipment()
        {
            var equippedItemsCopy = new Dictionary<EquipmentSlot, EquipmentItem>(_equippedItems);
            _equippedItems.Clear();
            foreach (var kvp in equippedItemsCopy)
            {
                OnUnEquipped?.Invoke(this, kvp.Key, kvp.Value);
            }
        }
    }
}
