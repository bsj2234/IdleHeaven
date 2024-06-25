using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public ItemViewModel ItemViewModel;

    [SerializeField] TMPro.TMP_Text itemName;
    [SerializeField] TMPro.TMP_Text itemDescription;
    [SerializeField] TMPro.TMP_Text itemPrice;
    [SerializeField] Image itemImage;
    [SerializeField] Image _equipedIcon;
    private UIButtonHoldDetector _buttonHoldable;

    private void Awake()
    {
        ItemViewModel.PropertyChanged += ItemViewModel_PropertyChanged;
        if(TryGetComponent(out UIButtonHoldDetector buttonHoldable))
        {
            _buttonHoldable = buttonHoldable;
        }
    }

    public ItemView RegisterOnClick(Action<Item> onClick)
    {
        if (_buttonHoldable == null)
        {
            Debug.LogError("Button holdable detector is not found");
            return this;
        }
        _buttonHoldable.onClick.AddListener(() => onClick(ItemViewModel.Item));
        return this;
    }
    public void RegisterOnHoldUp(Action<Item> itemHoldCallback)
    {
        if(_buttonHoldable == null)
        {
            Debug.LogError("Button holdable detector is not found");
            return;
        }
        _buttonHoldable.onHoldUp.AddListener(() => itemHoldCallback(ItemViewModel.Item));
    }

    public void SetItem(Item item)
    {
       ItemViewModel.Item = item;
        UpdateItemView(item);
    }

    public virtual void ItemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ItemViewModel.Item):
                UpdateItemView(sender as Item);
                break;
            default:
                break;
        }
    }

    private void UpdateItemView(Item item)
    {
        if(item == null)
        {
            itemImage.gameObject.SetActive(false);
            return;
        }
        else
        {
            itemImage.gameObject.SetActive(true);
        }
        if(itemName != null)
        {
            itemName.text = item.Name;
        }
        if(itemDescription != null)
        {
            itemDescription.text = item.ItemData.Description;
        }
        if(itemImage != null)
        {
            itemImage.sprite = item.ItemData.Icon;
        }
        if(_equipedIcon != null && item is EquipmentItem equipment)
        {
            _equipedIcon.gameObject.SetActive(equipment.Equiped);
        }
    }

}
