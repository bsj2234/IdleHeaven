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
        [SerializeField] ItemDetailView _itemPopupView;

        [SerializeField] Button Button_left;
        [SerializeField] Button Button_right;

        private string _filter = "all";
        private string _sortType = "none";
        private bool _descending = false;


        private void Start()
        {
            for (int i = 0; i < _itemViews.Length; i++)
            {
                _itemViews[i].RegisterOnClick(ItemClickCallback);
                _itemViews[i].RegisterOnHoldUp(ItemHoldCallback);
            }
            _inventoryViewModel.PropertyChanged += HandlePropertyChange;

            if(Button_left != null && Button_right != null)
            {
                Button_left.onClick.AddListener(OnLeftButtonClick);
                Button_right.onClick.AddListener(OnRightButtonClick);
            }

            UpdateInventoryView();
        }


        private void UpdateInventoryView()
        {
            List<Item> items = _inventoryViewModel.GetItemList(_filter,_sortType,_descending);

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
            if(itemView.ItemViewModel.Item == null)
            {
                return;
            }
            _itemPopupView.Init(itemView.ItemViewModel.Item);
            _itemPopupView.Window.Open();

            //RectTransform popupRectTransform = _itemPopupView.GetComponent<RectTransform>();
            //RectTransform parentRectTransform = _itemPopupView.transform.parent.GetComponent<RectTransform>();

            //// Convert the itemView position to the local position of the parent UI element
            //Vector2 localPoint;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, itemView.transform.position, null, out localPoint);
            //popupRectTransform.localPosition = localPoint;

            //// Ensure the popup is within the parent bounds
            //Vector3[] parentCorners = new Vector3[4];
            //parentRectTransform.GetWorldCorners(parentCorners);

            //Vector3[] popupCorners = new Vector3[4];
            //popupRectTransform.GetWorldCorners(popupCorners);

            //Vector2 offset = Vector2.zero;

            //if (popupCorners[0].x < parentCorners[0].x) // Left side
            //{
            //    offset.x = parentCorners[0].x - popupCorners[0].x;
            //}
            //else if (popupCorners[2].x > parentCorners[2].x) // Right side
            //{
            //    offset.x = parentCorners[2].x - popupCorners[2].x;
            //}

            //if (popupCorners[0].y < parentCorners[0].y) // Bottom side
            //{
            //    offset.y = parentCorners[0].y - popupCorners[0].y;
            //}
            //else if (popupCorners[1].y > parentCorners[1].y) // Top side
            //{
            //    offset.y = parentCorners[1].y - popupCorners[1].y;
            //}

            //popupRectTransform.localPosition += (Vector3)offset;
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
            _itemDetailView.Init(item);
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


        #region Sort Buttons
        public void OnSortByNoneButtonClicked()
        {
            _sortType = "none";
            UpdateInventoryView();
        }
        public void OnSortByAttackButtonClicked()
        {
            _sortType = "attack";
            UpdateInventoryView();
        }
        public void OnSortByDefenseButtonClicked()
        {
            _sortType = "defense";
            UpdateInventoryView();
        }
        public void OnSortByRarityButtonClicked()
        {
            _sortType = "rarity";
            UpdateInventoryView();
        }
        public void OnSortByNameButtonClicked()
        {
            _sortType = "name";
            UpdateInventoryView();
        }
        public void OnSortByTypeButtonClicked()
        {
            _sortType = "type";
            UpdateInventoryView();
        }
        public void OnSortByCritChanceButtonClicked()
        {
            _sortType = "critChance";
            UpdateInventoryView();
        }
        public void OnSortByCritDamageButtonClicked()
        {
            _sortType = "critDamage";
            UpdateInventoryView();
        }
        #endregion

        #region Filter Buttons
        public void OnFilterAllButtonClicked()
        {
            _filter = "all";
            UpdateInventoryView();
        }
        public void OnFilterEquipmentButtonClicked()
        {
            _filter = "equipment";
            UpdateInventoryView();
        }
        public void OnFilterWeaponButtonClicked()
        {
            _filter = "weapon";
            UpdateInventoryView();
        }
        public void OnFilterArmorButtonClicked()
        {
            _filter = "armor";
            UpdateInventoryView();
        }
        public void OnFilterUsableButtonClicked()
        {
            _filter = "usable";
            UpdateInventoryView();
        }
        #endregion
    }
}

