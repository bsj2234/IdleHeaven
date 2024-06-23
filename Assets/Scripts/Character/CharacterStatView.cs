using IdleHeaven;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class CharacterStatView : MonoBehaviour
{
    [SerializeField] CharacteStatsViewModel _viewModel;
    [SerializeField] List<HrizontalNamedLable> values;


    private void OnEnable()
    {
        if(_viewModel == null)
        {
            _viewModel = new CharacteStatsViewModel();

        }
        _viewModel.Init(ViewModel_PropertyChanged);
    }

    private void OnDisable()
    {
        _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "Stat":
                UpdateStatView(sender as Stat);
                break;
            default:
                break;
        }
    }

    private void UpdateStatView(Stat stat)
    {
        values[(int)stat.StatType].Name.text = stat.StatType.ToString();
        values[(int)stat.StatType].Value.text = stat.Value.ToString();
    }

    public void TestMVVM()
    {
        foreach(var stat in _viewModel._characterStats.Stats.stats)
        {
            stat.Value += 10;
        }
    }
}
