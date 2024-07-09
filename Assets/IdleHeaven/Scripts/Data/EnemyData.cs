using IdleHeaven;
using System;
using UnityEngine;

[Serializable]
public class EnemyData : IKeyProvider
{
    [SerializeField] string _name;
    [SerializeField] float _health;
    [SerializeField] float _attack;
    [SerializeField] float _defense;
    [SerializeField] GameObject _prefab;

    public string Name { get => _name; set => _name = value; }
    public string EnemyType { get; set; }
    public float BaseHealth { get; set; }
    public float BaseAttack { get; set; }
    public float BaseDefense { get; set; }
    public float BaseSpeed { get; set; }
    public string PrefabPath
    {
        set
        {
            Prefab = Resources.Load<GameObject>(value);
        }
    }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }

    public string GetKey()
    {
        return Name;
    }

    private Stats _stats;
    public Stats GetStats()
    {
        if(_stats == null)
        {
            _stats = new Stats();
            _stats.AddStat(StatType.Hp, BaseHealth);
            _stats.AddStat(StatType.Attack, BaseAttack);
            _stats.AddStat(StatType.Defense, BaseDefense);
            _stats.AddStat(StatType.Speed, BaseSpeed);
        }
        return _stats;
    }
}