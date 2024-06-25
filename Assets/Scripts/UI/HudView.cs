using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    [SerializeField] private HudViewModel _viewModel;
    
    [SerializeField] private Slider Slider_Hp;

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
                Slider_Hp.value = _viewModel.HpPercentage;
                break;
            case nameof(HudViewModel.MaxHp):
                Slider_Hp.value = _viewModel.HpPercentage;
                break;
            case nameof(HudViewModel.HpPercentage):
                Slider_Hp.value = _viewModel.HpPercentage;
                break;
            default:
                break;
        }
    }
}
