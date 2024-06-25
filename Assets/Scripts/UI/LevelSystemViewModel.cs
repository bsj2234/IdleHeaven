using System.ComponentModel;
using UnityEngine;
using IdleHeaven;

public class LevelSystemViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private LevelSystem _levelSystem;
    [SerializeField] CharacterStats _stats;

    public event PropertyChangedEventHandler PropertyChanged;

    public LevelSystem LevelSystem
    {
        get => _levelSystem;
        set
        {
            if (_levelSystem == value)
            {
                return;
            }

            // Unsubscribe from the old item
            if (_levelSystem != null)
            {
                _levelSystem.OnLevelSystemChanged -= HandleLevelSystemChanged;
            }
            _levelSystem = value;
            if (_levelSystem != null)
            {
                _levelSystem.OnLevelSystemChanged += HandleLevelSystemChanged;
            }
        }
    }

    private void Start()
    {
        LevelSystem = _stats.LevelSystem;
    }

    private void HandleLevelSystemChanged()
    {
        if (PropertyChanged != null)
        {
            PropertyChanged.Invoke(LevelSystem, new PropertyChangedEventArgs(nameof(LevelSystem)));
        }
    }
}
