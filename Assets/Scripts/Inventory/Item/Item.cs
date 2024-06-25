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
        public virtual ItemData ItemData 
        {
            get { return _itemData; }
            set
            {
                _itemData = value;
                OnItemChanged?.Invoke();
            }
        }


        public Action OnItemChanged;

        public Item()
        {

        }
        public Item(string name, ItemData data)
        {
            _name = name;
            _itemData = data;
        }
    }

    [Serializable]
    public class EquipmentItem : Item
    {
        private const int MAX_ITEMEFFECT = 3;
        private bool _equiped = false;
        public EquipmentData ItemData => _itemData as EquipmentData;

        [SerializeField]
        private ItemEffect[] _effects = new ItemEffect[MAX_ITEMEFFECT];

        public ItemEffect[] Effects => _effects;

        public bool Equiped
        {
            get { return _equiped; }
            set 
            {
                _equiped = value;
                OnItemChanged?.Invoke();
            }
        }

       

        public int Level { get; private set; } = 1;


        public EquipmentItem(string name, EquipmentData data) : base(name, data)
        {
            for (int i = 0; i < MAX_ITEMEFFECT; i++)
            {
                _effects[i] = ItemEffectRandomizer.Instance.GetRandomEffect();
            }
        }
        public Stats GetStatBonus()
        {
            if (ItemData is EquipmentData equipmentData)
            {
                return equipmentData.BonusStats;
            }
            return null;
        }
        public Stats GetEffectStatBonus()
        {
            Stats stats = new Stats();
            foreach (var effect in _effects)
            {
                stats[effect.Stat] += effect.Value * Level * 1.1f;
            }
            return stats;
        }
        public bool TryEnhance(CurrencyInventory currencyInventory)
        {
            if (ItemData is EquipmentData equipmentData)
            {
                if (Level < 10)
                {
                    if (currencyInventory.TryUseGold(Level * 100))
                    {
                       Level++;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
