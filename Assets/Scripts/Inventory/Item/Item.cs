using System;
using UnityEngine;

namespace IdleHeaven
{
    [System.Serializable]
    public class Item
    {
        private Inventory _owner;

        public Inventory Owner
        {

            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        [SerializeField] private string _name;
        [SerializeField] protected ItemData _itemData;

        public string Name => _name;
        public virtual ItemData ItemData => _itemData;


        public event Action OnItemChanged;

        public Item(string name, ItemData data)
        {
            _name = name;
            _itemData = data;
        }
    }

    public class EquipmentItem : Item
    {
        public EquipmentData ItemData => _itemData as EquipmentData;
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
