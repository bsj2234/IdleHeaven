using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class CharacteStatsViewModel : INotifyPropertyChanged
{
    public CharacterStats _characterStats;

    public CharacteStatsViewModel()
    {
        if(_characterStats != null)
        {
            foreach (Stat stat in _characterStats.Stats.stats)
            {
                if (stat != null)
                    stat.StatChanged += HandleStatChanged;
            }
            Debug.LogError("CharacterStats is Not null");
        }
        else
        {
            Debug.LogError("CharacterStats is null");
        }
    }

    public void Init()
    {

        if (_characterStats != null)
        {
            foreach (Stat stat in _characterStats.Stats.stats)
            {
                if (stat != null)
                    stat.StatChanged += HandleStatChanged;
            }
            Debug.LogError("CharacterStats is Not null");
        }
        else
        {
            Debug.LogError("CharacterStats is null");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void HandleStatChanged(Stat Stat)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged.Invoke(Stat, new PropertyChangedEventArgs(nameof(Stat)));
        }
    }

}