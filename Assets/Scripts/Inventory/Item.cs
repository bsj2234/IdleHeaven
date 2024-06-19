using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IdleHeaven
{
    [System.Serializable]
    public class Item
    {
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        public ItemData ItemData { get; set; }
        [field: SerializeField]
        public Sprite Icon { get; set; }


        public Item(string name, Sprite icon, ItemData data)
        {
            Name = name;
            Icon = icon;
            ItemData = data;
        }
    }

    public class WeaponItem : Item
    {
        public int damage;

        public WeaponItem(string name, Sprite icon, ItemData data, int damage) : base(name, icon, data)
        {
            this.damage = damage;
        }
    }
    public class EquipementItem : Item
    {
        public int _defense;

        public EquipementItem(string name, Sprite icon, ItemData data, int defense) : base(name, icon, data)
        {
            this._defense = defense;
        }
    }

    [System.Serializable]
    public class ItemData
    {
        public GameObject ItemPrefab;
        public string Desctription;
    }
}