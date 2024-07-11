using System;
using UnityEngine;
namespace IdleHeaven
{
    [Serializable]
    public enum Rarity
    {
        None,
        Currency,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Error
    }





    [Serializable]
    public class ItemEffectData : IKeyProvider
    {
        [SerializeField] private int _effectId;
        [SerializeField] private StatType _stat;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _levelMultiplier;
        [SerializeField] private float _maxLevelMultiplier;
        public int EffectId { get { return _effectId; } set { _effectId = value; } }
        public StatType Stat { get { return _stat; } set { _stat = value; } }
        public Rarity Rarity { get { return _rarity; } set { _rarity = value; } }
        public float MinValue { get { return _minValue; } set { _minValue = value; } }
        public float MaxValue { get { return _maxValue; } set { _maxValue = value; } }
        public float LevelMultiplier { get { return _levelMultiplier; } set { _levelMultiplier = value; } }
        public float MaxLevelMultiplier { get { return _maxLevelMultiplier; } set { _maxLevelMultiplier = value; } }

        public string GetKey()
        {
            return $"{_stat.ToString()}{_rarity.ToString()}";
        }
    }
    [Serializable]
    public class ItemEffect
    {
        [SerializeField] private StatType _stat;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private float _value;
        public StatType Stat { get { return _stat; } set { _stat = value; } }
        public Rarity Rarity { get { return _rarity; } set { _rarity = value; } }
        public float Value { get { return _value; } set { _value = value; } }

        public float LevelMultiplier { get; set; }
        public float MaxLevelMultiplier { get; set; }

        public Color GetRarityColor()
        {
            switch (Rarity)
            {
                case Rarity.Common:
                    return Color.white;
                case Rarity.Uncommon:
                    return Color.green;
                case Rarity.Rare:
                    return Color.blue;
                case Rarity.Epic:
                    return Color.magenta;
                case Rarity.Legendary:
                    return Color.yellow;
            }
            return Color.black;
        }
    }
}
