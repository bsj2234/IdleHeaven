using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public enum StatType
    {
        Hp,
        Attack,
        Defense,
        Speed,
        AttackSpeed,
        CritChance,
        CritDamage,
    }
    public class Stats
    {
        private Dictionary<StatType, float> _stats = new Dictionary<StatType, float>();

        public Dictionary<StatType, float> Value => _stats;

        public float this[StatType statType]
        {
            get
            {
                if (_stats.TryGetValue(statType, out float value))
                {
                    return value;
                }
                return -1f;
            }
            set
            {
                if (_stats.ContainsKey(statType))
                {
                    _stats[statType] = value;
                }
                else
                {
                    _stats[statType] = value;
                }
            }
        }

        public void AddStat(Stats stats)
        {
            foreach (var stat in stats.Value)
            {
                AddStat(stat.Key, stat.Value);
            }
        }

        public void AddStat(StatType statType, float amount)
        {
            if (_stats.ContainsKey(statType))
            {
                _stats[statType] += amount;
            }
        }

        public void Clear()
        {
            _stats.Clear();
        }
    }
}

