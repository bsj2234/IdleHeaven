using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace IdleHeaven
{
    public enum StatType
    {
        Hp,
        Attack,
        Defense,
        Resistanse,
        Speed,
        AttackSpeed,
        CritChance,
        CritDamage
    }



    [System.Serializable]
    public class Stat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [SerializeField]
        private StatType _statType;
        [SerializeField]
        private float _value;

        public StatType StatType
        {
            get => _statType;
            set
            {
                _statType = value;
            }
        }

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public Stat(StatType statType, float value)
        {
            _statType = statType;
            _value = value;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    [System.Serializable]
    public class Stats
    {
        public List<Stat> stats;

        public Stats()
        {
            stats = new List<Stat>();
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                stats.Add(new Stat(statType, 0));
            }
        }

        public float this[StatType statType]
        {
            get
            {
                foreach (Stat stat in stats)
                {
                    if (stat.StatType == statType)
                    {
                        return stat.Value;
                    }
                }
                return 0;
            }
            set
            {
                for (int i = 0; i < stats.Count; i++)
                {
                    if (stats[i].StatType == statType)
                    {
                        stats[i].Value = value;
                        return;
                    }
                }
                Assert.IsTrue(false, "StatType not found");
            }
        }


        public void Clear()
        {
            foreach (Stat stat in stats)
            {
                stat.Value = 0;

            }
        }

        public void AddStats(Stats other)
        {
            foreach (Stat stat in other.stats)
            {
                this[stat.StatType] += stat.Value;
            }
        }
    }
}

