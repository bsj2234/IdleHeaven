using UnityEngine;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private int _hp;
    [SerializeField] private Dictionary<string, int> _stats;

    public int Level => _level;

    private void Awake()
    {
        _stats = new Dictionary<string, int>();
    }

    public int GetStatValue(string statName)
    {
        if (_stats.TryGetValue(statName, out int value))
        {
            return value;
        }
        return 0;
    }

    public void ChangeHp(int amount)
    {
        _hp += amount;
        Debug.Log($"HP changed by {amount}, new HP: {_hp}");
    }

    public void ChangeStat(string statName, int amount)
    {
        if (_stats.ContainsKey(statName))
        {
            _stats[statName] += amount;
        }
        else
        {
            _stats[statName] = amount;
        }
        Debug.Log($"{statName} changed by {amount}, new value: {_stats[statName]}");
    }
}
