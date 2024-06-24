using System;
using UnityEngine;
using IdleHeaven;

[Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] int _level;
    [SerializeField] float _health;
    [SerializeField] float _attack;
    [SerializeField] float _defense;
    [SerializeField] float _resistance;
    [SerializeField] Stats _stat;

    public string Name { get => _name; set => _name = value;}
    public int Level { get => _level; set => _level = value; }
    public float Health { get => _health; set => _health = value; }
    public float Attack { get => _attack; set => _attack = value; }
    public float Defense { get => _defense; set => _defense = value; }
    public float Resistance { get => _resistance; set => _resistance = value; }
    public Stats Stat
    {
        get
        {
            if (_stat == null)
            {
                _stat = new Stats()
                    .AddStat(StatType.Hp, Health)
                    .AddStat(StatType.Attack, Attack)
                    .AddStat(StatType.Defense, Defense)
                    .AddStat(StatType.Resistance, _resistance);
            }
            return _stat;
        }
    }
}