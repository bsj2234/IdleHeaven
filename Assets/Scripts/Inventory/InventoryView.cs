using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;


namespace IdleHeaven
{
    public class InventoryView : MonoBehaviour
    {
        private InventoryViewModel _viewModel;

        [SerializeField] Inventory _inventory;
        [SerializeField] Equipments _equipments;
        [SerializeField] ItemView[] _itemViews;



        private void Start()
        {
            _viewModel = new InventoryViewModel(_inventory);
            _viewModel.PropertyChanged += HandlePropertyChanged;
            for(int i = 0; i < _itemViews.Length; i++)
            {
                _viewModel.itemViewModels[i] = _itemViews[i].ItemViewModel;
            }
        }
        public void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(Inventory):
                    UpdateInventoryView(sender as Inventory);
                    break;
            }
        }

        private void UpdateInventoryView(Inventory inventory)
        {
            for (int i = 0; i < inventory.INVENTORY_SIZE; i++)
            {
                if(inventory.Items != null && i < inventory.Items.Count)
                {
                    Item currentItem = inventory.Items[i];
                    _viewModel.itemViewModels[i].Item = currentItem;
                }
                else
                {
                    _viewModel.itemViewModels[i].Item  = null;
                }
            }
        }

        private void HandleItemClicked(Item item)
        {
            if(item is EquipmentItem equipment)
            {
               _equipments.Equip(equipment.ItemData.EquipmentSlot ,equipment);
            }
        }

    }
}

