using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

public class CharacteStatsViewModel : Notifier
{
    public Stats _characterStats;

    public CharacteStatsViewModel()
    {
        foreach (var stat in _characterStats.stats)
        {
            stat.PropertyChanged += OnPlayerStatChanged;
        }
    }

    public void OnPlayerStatChanged(object sender, PropertyChangedEventArgs e)
    {
        Debug.Log("Stat changed: " + e.PropertyName);
    }

}