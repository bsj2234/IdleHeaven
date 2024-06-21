using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield }

    public class Equipments : MonoBehaviour
    {
        [SerializeField] private EquipmentItem[] _slotVisualize;
        private Dictionary<EquipmentSlot, EquipmentItem> _equippedItems = new Dictionary<EquipmentSlot, EquipmentItem>();

        public event Action<Equipments, EquipmentSlot, EquipmentItem> OnEquipped;
        public event Action<Equipments, EquipmentSlot, EquipmentItem> OnUnEquipped;

        public EquipmentItem Equip(EquipmentSlot slot, EquipmentItem item)
        {
            EquipmentItem previousItem = null;
            if (_equippedItems.ContainsKey(slot))
            {
                previousItem = _equippedItems[slot];
                _equippedItems[slot] = item;
            }
            else
            {
                _equippedItems.Add(slot, item);
            }

            OnEquipped?.Invoke(this, slot, item);
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
