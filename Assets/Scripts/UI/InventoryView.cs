using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


namespace IdleHeaven
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] InventoryViewModel _inventoryViewModel;
        [SerializeField] ItemView[] _itemViews;
        [SerializeField] ItemDetailView _itemDetailView;
        [SerializeField] ItemView _itemPopupView;

        [SerializeField] Button Button_left;
        [SerializeField] Button Button_right;


        private void Start()
        {
            for (int i = 0; i < _itemViews.Length; i++)
            {
                _itemViews[i].RegisterOnClick(ItemClickCallback);
                _itemViews[i].RegisterOnHoldUp(ItemHoldCallback);
            }
            _inventoryViewModel.PropertyChanged += HandlePropertyChange;

            Button_left.onClick.AddListener(OnLeftButtonClick);
            Button_right.onClick.AddListener(OnRightButtonClick);

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

        private void ShowDetailView(ItemView itemView)
        {
            _itemPopupView.Init(itemView);
            _itemPopupView.Window.Open();
            _itemPopupView.transform.position = itemView.transform.position;
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

        private void ItemClickCallback(ItemView itemView)
        {
            //if (item is EquipmentItem equipment)
            //{
            //    _inventoryViewModel.Equip(equipment);
            //}
            ShowDetailView(itemView);
        }
        private void ItemHoldCallback(Item item)
        {
            Debug.Log("holded");

            _itemDetailView.Window.Open();
            _itemDetailView.OnOpen(item);
        }

        private void OnLeftButtonClick()
        {
            _inventoryViewModel.GetPreviousPage();
            UpdateInventoryView();
        }
        private void OnRightButtonClick()
        {
            _inventoryViewModel.GetNextPage();
            UpdateInventoryView();
        }
    }
}

