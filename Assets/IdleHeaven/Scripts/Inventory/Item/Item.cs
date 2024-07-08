using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace IdleHeaven
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] private Inventory _owner;
        [SerializeField] private string _name;
        [SerializeField] private int _quantity;
        [SerializeField] protected ItemData _itemData;

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
        public string Name => _name;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnItemChanged?.Invoke();
            }
        }
        public virtual ItemData ItemData
        {
            get { return _itemData; }
            set
            {
                _itemData = value;
                OnItemChanged?.Invoke();
            }
        }

        [JsonIgnore]
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
        [SerializeField] int _level = 1;
        [SerializeField] int _enhancedLevel = 1;
        [SerializeField] ItemEffect[] _effects = new ItemEffect[MAX_ITEMEFFECT];
        [SerializeField] Stats _baseStats = new Stats();
        [SerializeField] Stats _resultStats = new Stats();
        private RarityData _rarityData;
        public RarityData RarityData
        {
            get => _rarityData;
            set
            {
                _rarityMultiply = Random.Range(value.MinBaseStatMulti, value.MaxBaseStatMulti);
                _rarityData = value;
            }
        }
        private float _rarityMultiply;

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
        public Stats ResultStats
        {
            get
            {
                return _resultStats;
            }
        }
        public Stats BaseStats => _baseStats;
        public EquipmentData EquipmentData
        {
            set
            {
                _itemData = value as EquipmentData;
                Assert.IsNotNull(_itemData);
            }
            get
            {
                Assert.IsNotNull(_itemData);
                return _itemData as EquipmentData;
            }

        }

        public EquipmentItem(string name, ItemData data, int level) : base(name, data, 1)
        {
            _level = level;
            OnItemChanged += RecalcResultStats;
        }

        public void SetRandomEffects()
        {
            for (int i = 0; i < MAX_ITEMEFFECT; i++)
            {
                _effects[i] = ItemEffectRandomizer.Instance.GetRandomEffect();
            }
            OnItemChanged?.Invoke();
            foreach (Stat stat in _baseStats.stats)
            {
                stat.RegisterStatChanged(HandleStatChange);
            }
        }
        public void HandleStatChange(Stat stat)
        {
            RecalcResultStats();
        }
        public void RecalcResultStats()
        {
            _resultStats.Clear();
            _resultStats.AddStats(_baseStats);
            foreach (ItemEffect effect in _effects)
            {
                _resultStats[effect.Stat] += GetEffectStat(effect);
            }

            foreach (Stat stat in _resultStats.stats)
            {
                stat .Value *= _rarityMultiply;
            }
        }
        public float GetEffectStat(ItemEffect effect)
        {
            return effect.Value * EnhancedLevel * 1.1f * Level * 0.02f;
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

        public float GetItemBattleRating()
        {
            float result = 0f;
            // 장비는 공격력, 크리티컬 찬수, 데미지가 0일 수 있어 1씩 더해줌
            // 플레이어 스탯 배틀레이팅과 다름
            float damage = 1f + ResultStats[StatType.Attack];
            float criticalMulti = (1f + (1f + ResultStats[StatType.CritDamage]) * ResultStats[StatType.CritChance]);
            result = (1f + ResultStats[StatType.AttackSpeed]) * criticalMulti * damage;
            return result;
        }
    }
}