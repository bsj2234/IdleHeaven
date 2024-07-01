using IdleHeaven;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public ItemViewModel ItemViewModel;

    [SerializeField] private Image Image_item;
    [SerializeField] private Image Image_equipedIcon;

    [SerializeField] private TMP_Text TEXT_itemName;
    [SerializeField] private TMP_Text TEXT_itemDescription;
    [SerializeField] private TMP_Text TEXT_itemQuantity;

    [SerializeField] private HorizontalNamedLable[] Text_stats;

    [SerializeField] private HorizontalNamedLable[] Text_effects;

    private UIButtonHoldDetector _buttonHoldable;

    public UIWindow Window;

    private void Awake()
    {
        ItemViewModel.PropertyChanged += ItemViewModel_PropertyChanged;
        if (TryGetComponent(out UIButtonHoldDetector buttonHoldable))
        {
            _buttonHoldable = buttonHoldable;
        }
    }
    private void OnEnable()
    {
        UpdateItemView(ItemViewModel.Item);
    }


    public void Init(ItemView itemView)
    {
        ItemViewModel = itemView.ItemViewModel;
        UpdateItemView(ItemViewModel.Item);
    }

    public ItemView RegisterOnClick(Action<ItemView> onClick)
    {
        if (_buttonHoldable == null)
        {
            Debug.LogError($"Button holdable detector is not found {gameObject.name}, {transform.parent.name}");
            return this;
        }
        _buttonHoldable.onClick.AddListener(() => onClick(this));
        return this;
    }
    public void RegisterOnHoldUp(Action<Item> itemHoldCallback)
    {
        if (_buttonHoldable == null)
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
        ClearFields();
        if (item == null)
        {
            Image_item.gameObject.SetActive(false);
            return;
        }
        else
        {
            Image_item.gameObject.SetActive(true);
        }


        TrySetText(TEXT_itemName, item.Name);
        //TrySetText(TEXT_itemDescription, item.Description);
        TrySetText(TEXT_itemQuantity, item.Quantity.ToString());

        TrySetImage(Image_item, item.ItemData.Icon);
        //TrySetImage(Image_equipedIcon, item.Owner != null ? item.Owner.Icon : null);


        if (item is EquipmentItem equipmentItem)
        {
            for (int i = 0; i < Text_effects.Length; i++)
            {
                TrySetText(Text_effects[i].Name, equipmentItem.Effects[i].Stat.ToString());
                TrySetText(Text_effects[i].Value, equipmentItem.Effects[i].Value.ToString());

                Text_effects[i].Name.color = equipmentItem.Effects[i].GetRarityColor();
            }
            for (int i = 0; i < Text_stats.Length; i++)
            {
                TrySetText(Text_stats[i].Name, ((StatType)i).ToString());
                TrySetText(Text_stats[i].Value, equipmentItem.ResultStats[(StatType)i].ToString());
                Text_stats[i].Name.color = Color.white;
                Text_stats[i].Value.color = Color.white;
            }

            for (int i = 0; i < equipmentItem.Effects.Length; i++)
            {
                if ((int)equipmentItem.Effects[i].Stat >= Text_stats.Length)
                {
                    continue;
                }
                Text_stats[(int)equipmentItem.Effects[i].Stat].Name.color = equipmentItem.Effects[i].GetRarityColor();
                Text_stats[(int)equipmentItem.Effects[i].Stat].Value.color = equipmentItem.Effects[i].GetRarityColor();
            }
        }
    }

    private void ClearFields()
    {
        TrySetText(TEXT_itemName, "");
        TrySetText(TEXT_itemDescription, "");
        TrySetText(TEXT_itemQuantity, "");

        TrySetImage(Image_item, null);
        TrySetImage(Image_equipedIcon, null);

        foreach (var text in Text_effects)
        {
            TrySetText(text.Value, "");
        }
        foreach (var text in Text_stats)
        {
            TrySetText(text.Value, "");
        }
    }

    private void TrySetText(TMP_Text text, string contentText)
    {
        if (text != null)
        {
            text.text = contentText;
        }
    }

    private void TrySetImage(Image image, Sprite sprite)
    {
        if (image != null)
        {
            image.sprite = sprite;
        }
    }

}
