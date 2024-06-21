using System;
using UnityEngine;

namespace IdleHeaven
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] private string _name;
        [SerializeField] private ItemData _itemData;

        public string Name => _name;
        public ItemData ItemData => _itemData;


        public event Action OnItemChanged;

        public Item(string name, ItemData data)
        {
            _name = name;
            _itemData = data;
        }
    }

    public class EquipmentItem : Item
    {
        public EquipmentItem(string name, EquipmentData data) : base(name, data)
        {
        }
        public Stats GetStatBonus()
        {
            if (ItemData is EquipmentData equipmentData)
            {
                return equipmentData.BonusStats;
            }
            return null;
        }
    }
}
