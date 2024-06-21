using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public ItemViewModel ItemViewModel;

    public TMPro.TMP_Text itemName;
    public TMPro.TMP_Text itemDescription;
    public TMPro.TMP_Text itemPrice;
    public Image itemImage;
    private Button _button;

    private void Awake()
    {
        ItemViewModel = new ItemViewModel();
        ItemViewModel.PropertyChanged += ItemViewModel_PropertyChanged;
        if(TryGetComponent(out Button button))
        {
            _button = button;
            _button.onClick.AddListener(HandleClick);
        }
    }
    private void OnDestroy()
    {
        if(_button != null)
        {
            _button.onClick.RemoveListener(HandleClick);
        }
    }

    public void SetItem(Item item)
    {
       ItemViewModel.Item = item;
    }

    public virtual void HandleClick()
    {
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
            return;
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
    }
}
