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
        }
    }

    public ItemView Init(Action<Item> onClick)
    {
        if(_button == null)
        {

           Debug.LogError("Button is not found");
            return this;
        }
        _button.onClick.AddListener(() => onClick(ItemViewModel.Item));
        return this;
    }

    private void OnDestroy()
    {
        if(_button != null)
        {
        }
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
    }
}
