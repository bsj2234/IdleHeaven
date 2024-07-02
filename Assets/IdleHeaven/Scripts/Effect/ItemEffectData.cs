using System;
using UnityEngine;
namespace IdleHeaven
{
    [Serializable]
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Error
    }


    public class RarityData
    {
        private static readonly RarityData[] rarityData = new RarityData[]
        {
        new RarityData(Rarity.Common, "Common", Color.gray, 1f, 1f),
        new RarityData(Rarity.Uncommon, "Uncommon", Color.green, 1f, 1.1f),
        new RarityData(Rarity.Rare, "Rare", Color.blue, 1.1f, 1.5f),
        new RarityData(Rarity.Epic, "Epic", Color.magenta, 1.5f, 2f),
        new RarityData(Rarity.Legendary, "Legendary", Color.yellow, 2f, 3f)
        };

        public Rarity Rarity { get; private set; }
        public string Name { get; private set; }
        public Color Color { get; private set; }

        public float MinBaseStatMulti { get; private set; }
        public float MaxBaseStatMulti { get; private set; }

        private RarityData(Rarity rarity, string name, Color color, float minBaseStatMulti, float maxBaseStatMulti)
        {
            Rarity = rarity;
            Name = name;
            Color = color;
            MinBaseStatMulti = minBaseStatMulti;
            MaxBaseStatMulti = maxBaseStatMulti;
        }

        public static RarityData GetRarityData(Rarity rarity)
        {
            return rarityData[(int)rarity];
        }

        public static string GetRarityName(Rarity rarity)
        {
            return GetRarityData(rarity).Name;
        }

        public static Color GetRarityColor(Rarity rarity)
        {
            return GetRarityData(rarity).Color;
        }
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
