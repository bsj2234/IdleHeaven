using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class CharacteStatsViewModel : INotifyPropertyChanged
{
    public CharacterStats _characterStats;

    public void Init(PropertyChangedEventHandler OnPropertyChanged)
    {
        PropertyChanged += OnPropertyChanged;

        if (_characterStats != null)
        {
            foreach (Stat stat in _characterStats.Stats.stats)
            {
                if (stat != null)
                {
                    stat.RegisterStatChanged(HandleStatChanged);
                }
            }
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