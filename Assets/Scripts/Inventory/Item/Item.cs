using System;
using UnityEngine;

namespace IdleHeaven
{
    [System.Serializable]
    public class Item
    {
        private Inventory _owner;

        private int _quantity;

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
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnItemChanged?.Invoke();
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
            _quantity = 1;
            OnItemChanged?.Invoke();
        }
        public Item(int quantity)
        {
            _quantity = quantity;
            OnItemChanged?.Invoke();
        }
        public Item(string name, ItemData data, int quantity = 1) : this(quantity)
        {
            _name = name;
            _itemData = data;
        }
    }

    [Serializable]
    public class EquipmentItem : Item
    {
        private const int MAX_ITEMEFFECT = 3;
        [SerializeField] bool _equiped = false;
        [SerializeField] float _damage;
        [SerializeField] float _defense;
        [SerializeField] int _level;
        [SerializeField] int _enhancedLevel;
        [SerializeField] ItemEffect[] _effects = new ItemEffect[MAX_ITEMEFFECT];
        [SerializeField] Stats _baseStats = new Stats();
        [SerializeField] Stats _resultStats = new Stats();
        public Rarity Rarity;
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
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
            }
        }
        public int EnhancedLevel
        {
            get { return _enhancedLevel; }
            set
            {
                _enhancedLevel = value;
                OnItemChanged?.Invoke();
            }
        }
        public float Damage
        {
            get { return _damage; }
            set
            {
                _damage = value;
                _resultStats[StatType.Attack] = _damage;
            }
        }
        public float Defense
        {
            get { return _defense; }
            set
            {
                _defense = value;
                _resultStats[StatType.Defense] = _defense;
            }
        }
        public Stats Stats => _resultStats;
        public Stats BaseStats => _baseStats;
        public EquipmentData EquipmentData => ItemData as EquipmentData;



        public EquipmentItem(string name, ItemData data) : base(name, data, 1)
        {
            for (int i = 0; i < MAX_ITEMEFFECT; i++)
            {
                _effects[i] = ItemEffectRandomizer.Instance.GetRandomEffect();
            }
            RecalcResultStats();
            OnItemChanged += RecalcResultStats;
        }
        public void RecalcResultStats()
        {
            _resultStats.Clear();
            _resultStats.AddStats(_baseStats);
            foreach (var effect in _effects)
            {
                _resultStats[effect.Stat] += effect.Value * EnhancedLevel * 1.1f * Level * 0.02f;
            }
        }
        public bool TryEnhance(CurrencyInventory currencyInventory)
        {
            if (ItemData is EquipmentData equipmentData)
            {
                if (EnhancedLevel < 10)
                {
                    if (currencyInventory.TryUseGold(EnhancedLevel * 100))
                    {
                        EnhancedLevel++;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
