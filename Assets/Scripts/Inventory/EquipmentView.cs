using IdleHeaven;
using System;
using System.ComponentModel;
using UnityEngine;

public class EquipmentView : MonoBehaviour
{
    [SerializeField] EquipmentViewModel _equipmentViewModel;
    [SerializeField] ItemView[] _itemViews;
    public int slotsCount = Enum.GetNames(typeof(EquipmentSlot)).Length;

    private void Start()
    {
        _equipmentViewModel.PropertyChanged += HandlePropertyChanged;

        foreach (var view in _itemViews)
        {
            view.RegisterOnClick(ItemClickCallback);
        }
    }

    private void UpdateEquipmentsView()
    {
        for (int i = 0; i < _itemViews.Length; i++)
        {
            Item equipedItem = _equipmentViewModel.GetItem((EquipmentSlot)i);
            if (equipedItem == null)
            {
                _itemViews[i].SetItem(null);
                continue;
            }
            _itemViews[i].SetItem(equipedItem);
        }
    }
    private void ItemClickCallback(Item item)
    {
        Debug.Log("clicked");
        EquipmentItem equipmentItem = item as EquipmentItem;
        _equipmentViewModel.Unequip(equipmentItem.ItemData.EquipmentSlot);
    }


    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(EquipmentViewModel.Equipments):
                UpdateEquipmentsView();
                break;
            default:
                break;
        }
    }
}
