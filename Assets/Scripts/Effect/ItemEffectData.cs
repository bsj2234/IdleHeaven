using IdleHeaven;
using System;
using UnityEngine;
namespace IdleHeaven
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Error
    }

    [Serializable]
    public class ItemEffectData
    {
        [SerializeField] private int _effectId;
        [SerializeField] private StatType _stat;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;
        public int EffectId { get { return _effectId; } set { _effectId = value; } }
        public StatType Stat { get { return _stat; } set { _stat = value; } }
        public Rarity Rarity { get { return _rarity; } set { _rarity = value; } }
        public int MinValue { get { return _minValue; } set { _minValue = value; } }
        public int MaxValue { get { return _maxValue; } set { _maxValue = value; } }
    }
    [Serializable]
    public class ItemEffect
    {
        [SerializeField] private StatType _stat;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private int _value;
        public StatType Stat { get { return _stat; } set { _stat = value; } }
        public Rarity Rarity { get { return _rarity; } set { _rarity = value; } }
        public int Value { get { return _value; } set { _value = value; } }
    }
}
