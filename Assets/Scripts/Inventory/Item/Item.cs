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

        public Item(string name, ItemData data)
        {
            _name = name;
            _itemData = data;
        }
    }

    [System.Serializable]
    public class WeaponItem : Item
    {
        [SerializeField] private int _damage;

        public int Damage => _damage;

        public WeaponItem(string name, ItemData data, int damage) : base(name, data)
        {
            _damage = damage;
        }
    }

    [System.Serializable]
    public class EquipmentItem : Item
    {
        [SerializeField] private int _defense;

        public int Defense => _defense;

        public EquipmentItem(string name, ItemData data, int defense) : base(name, data)
        {
            _defense = defense;
        }
    }
}
