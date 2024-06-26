using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    [SerializeField] private HudViewModel _viewModel;
    
    [SerializeField] Slider Slider_hp;
    [SerializeField] TMP_Text Text_hp;
    [SerializeField] TMP_Text Text_maxHp;

    private void Start()
    {
        _viewModel.PropertyChanged += HandlePropertyChanged;
    }
    private void OnDestroy()
    {
        _viewModel.PropertyChanged -= HandlePropertyChanged;
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(HudViewModel.Hp):
                Slider_hp.value = _viewModel.HpPercentage;
                Text_hp.text = _viewModel.Hp.ToString();
                Text_maxHp.text = _viewModel.MaxHp.ToString();
                break;
            case nameof(HudViewModel.MaxHp):
                Slider_hp.value = _viewModel.HpPercentage;
                Text_hp.text = _viewModel.Hp.ToString();
                Text_maxHp.text = _viewModel.MaxHp.ToString();
                break;
            case nameof(HudViewModel.HpPercentage):
                Slider_hp.value = _viewModel.HpPercentage;
                Text_hp.text = _viewModel.Hp.ToString();
                Text_maxHp.text = _viewModel.MaxHp.ToString();
                break;
            default:
                break;
        }
    }
}
