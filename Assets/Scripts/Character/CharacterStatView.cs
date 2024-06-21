using IdleHeaven;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class CharacterStatView : MonoBehaviour
{
    [SerializeField] CharacterStats _characterStats;
    [SerializeField] List<HrizontalNamedLable> valuse;

    

    private void OnEnable()
    {
        foreach (var stat in _characterStats.Stats.stats)
        {
            stat.PropertyChanged += Stat_OnStatChanged;
        }
    }

    private void OnDisable()
    {
        foreach (var stat in _characterStats.Stats.stats)
        {
            stat.PropertyChanged -= Stat_OnStatChanged;
        }
    }

    private void Stat_OnStatChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "Value":
                UpdateStatView(sender as Stat);
                break;
            default:
                break;
        }
    }

    private void UpdateStatView(Stat stat)
    {
        valuse[(int)stat.StatType].Name.text = stat.StatType.ToString();
        valuse[(int)stat.StatType].Value.text = stat.Value.ToString();
    }

    public void TestMVVM()
    {
        foreach (var stat in _characterStats.Stats.stats)
        {
            stat.Value += 1;
        }
    }
}
