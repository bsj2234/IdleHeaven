using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemAquiredView : MonoBehaviour
{
    [SerializeField] private ItemAquiredViewModel _viewModel;
    [SerializeField] private UiDrawerEffect _drawer;

    private bool _isPlaying = false;

    private Queue<Item> _aquiredItems = new Queue<Item>();

    [SerializeField] private TMP_Text Text_itemName;
    [SerializeField] private TMP_Text[] Text_itemEffect;

    private void Start()
    {
        _viewModel.Init();
        _viewModel.OnItemAquirePopup += OnItemAquirePopup;
        _drawer.OnDrawerClosed += OnDrawerClosed;
    }
    private void OnItemAquirePopup(Item item)
    {
        if (_isPlaying)
        {
            _aquiredItems.Enqueue(item);
            return;
        }
        DisplayItem(item);
    }

    private void DisplayItem(Item item)
    {
        _isPlaying = true;

        ResetText();

        Text_itemName.text = item.Name;
        if(item is EquipmentItem eItem)
        {
            Text_itemName.color = eItem.RarityData.Color;

            for ( int i = 0; i < eItem.Effects.Length; i++)
            {
                Text_itemEffect[i].text = eItem.Effects[i].Stat.ToString();
                Text_itemEffect[i].color = eItem.Effects[i].GetRarityColor();
            }
        }

        _drawer.OpenWithDelayedClose(2f);
    }

    private void OnDrawerClosed()
    {
        if(_aquiredItems.Count == 0)
        {
            _isPlaying = false;
            return;
        }
        Item item = _aquiredItems.Dequeue();
        DisplayItem(item);
    }

    private void ResetText()
    {
        Text_itemName.text = "";
        Text_itemName.color = Color.white;

        foreach(TMP_Text text in Text_itemEffect)
        {
            text.text = "";
            text.color = Color.white;
        }
    }
}
