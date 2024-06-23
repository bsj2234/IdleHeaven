using IdleHeaven;
using System;
using System.ComponentModel;
using UnityEngine;

public class EquipmentView : MonoBehaviour
{
    private EquipmentViewModel _equipmentViewModel;
    [SerializeField] ItemView[] views;

    private void Start()
    {
        Init(new EquipmentViewModel(FindAnyObjectByType<Equipments>()));
    }

    public EquipmentView Init(EquipmentViewModel equipmentViewModel)
    {
        _equipmentViewModel = equipmentViewModel;

        foreach (var view in views)
        {
            view.Init(OnItemClick);
        }

        _equipmentViewModel.PropertyChanged += HandlePropertyChanged;

        return this;
    }

    private void OnItemClick(Item item)
    {
        Debug.Log("clicked");
        EquipmentItem equipmentItem = item as EquipmentItem;
        _equipmentViewModel.Equipments.Unequip(equipmentItem.ItemData.EquipmentSlot);
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(EquipmentViewModel.Equipments):
                UpdateEquipmentView();
                break;
        }
    }
    private void UpdateEquipmentView()
    {
        for (int i = 0; i < _equipmentViewModel.slotsCount; i++)
        {
            Item item = _equipmentViewModel.GetItem((EquipmentSlot)i);
            views[i].SetItem(item);
        }
    }
}
