using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;


namespace IdleHeaven
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] InventoryViewModel _inventoryViewModel;
        [SerializeField] ItemView[] _itemViews;
        [SerializeField] ItemDetailView _itemDetailUi;


        private void Start()
        {
            for (int i = 0; i < _itemViews.Length; i++)
            {
                _itemViews[i].RegisterOnClick(ItemClickCallback);
                _itemViews[i].RegisterOnHoldUp(ItemHoldCallback);
            }
            _inventoryViewModel.PropertyChanged += HandlePropertyChange;

            UpdateInventoryView();
        }

        private void UpdateInventoryView()
        {
            List<Item> items = _inventoryViewModel.GetFilteredItem("");

            for (int i = 0; i < _itemViews.Length; i++)
            {
                if (i >= items.Count)
                {
                    _itemViews[i].SetItem(null);
                    continue;
                }
                Item currentItem = items[i];
                if (currentItem != null)
                {
                    _itemViews[i].SetItem(currentItem);
                }
            }
        }
        private void HandlePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(InventoryViewModel.Inventory):
                    UpdateInventoryView();
                    break;
                default:
                    break;
            }
        }

        private void ItemClickCallback(Item item)
        {
            Debug.Log("clicked");
            if (item is EquipmentItem equipment)
            {
                _inventoryViewModel.Equip(equipment);
            }
        }
        private void ItemHoldCallback(Item item)
        {
            Debug.Log("holded");

            _itemDetailUi.Window.Open();
            _itemDetailUi.OnOpen(item);
        }
    }
}

