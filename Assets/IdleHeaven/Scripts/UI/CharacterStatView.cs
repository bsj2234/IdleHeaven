using IdleHeaven;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class CharacterStatView : MonoBehaviour
{
    [SerializeField] CharacteStatsViewModel _viewModel;
    [SerializeField] List<HorizontalNamedLable> HoriNamed_Stats;
    [SerializeField] HorizontalNamedLable HoriNamed_battleRating;


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
                UpdateRattleRating();
                break;
            default:
                break;
        }
    }

    private void UpdateRattleRating()
    {
        HoriNamed_battleRating.Value.text = _viewModel._characterStats.GetCharacterBattleRating().ToString("N2");
    }

    private void UpdateStatView(Stat stat)
    {
        if (HoriNamed_Stats[(int)stat.StatType] == null)
        {
            Debug.LogWarning("Stat not found");
            return;
        }
        HoriNamed_Stats[(int)stat.StatType].Name.text = stat.StatType.ToString();
        HoriNamed_Stats[(int)stat.StatType].Value.text = stat.Value.ToString("N2");
    }
}
