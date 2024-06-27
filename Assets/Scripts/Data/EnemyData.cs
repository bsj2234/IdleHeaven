using IdleHeaven;
using System;
using UnityEngine;

[Serializable]
public class EnemyData : IKeyProvider
{
    [SerializeField] string _name;
    [SerializeField] int _level;
    [SerializeField] float _health;
    [SerializeField] float _attack;
    [SerializeField] float _defense;
    [SerializeField] float _resistance;
    [SerializeField] Stats _stat;
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
}