using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace IdleHeaven
{
    public class InventoryViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] Inventory _inventory;

        [SerializeField] Equipments _equipments;

        public event PropertyChangedEventHandler PropertyChanged;

        public Inventory Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Inventory)));
                }
            }
        }


        private void Start()
        {
            _inventory.OnInventoryChanged += HandleInventoryChanged;
        }

        private void HandleInventoryChanged(Item item)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Inventory)));
            }
        }

        public void Equip(Item item)
        {
            if (item is EquipmentItem)
            {
                EquipmentItem equipmentItem = item as EquipmentItem;
                _equipments.Equip(equipmentItem);
            }
        }

        public List<Item> GetFilteredItem(string filter)
        {
            List<Item> items = Inventory.GetItems();
            List<Item> list = new List<Item>();

            if(filter == "equipment")
            {
                for(int i = 0; i < _itemCountPerPage; i++)
                {
                    int index = _currentPage * _itemCountPerPage + i;

                    if (items[index] is EquipmentItem)
                    {
                        list.Add(items[index]);
                    }
                }
                return list;
            }
            else
            {


                for (int i = 0; i < _itemCountPerPage; i++)
                {
                    int index = _currentPage * _itemCountPerPage + i;

                    //if (items[index] is EquipmentItem)
                    if(items.Count > index)
                    {
                        list.Add(items[index]);
                    }
                    else
                    {
                        break;
                    }
                }
                return list;
                //return _inventory.GetItems();
            }
        }

        private int _currentPage = 0;
        public int _itemCountPerPage = 20;

        public void GetPreviousPage()
        {
            if (_currentPage > 0)
            {
                _currentPage--;
            }
        }

        public void GetNextPage()
        {
            if (_currentPage < (Inventory.GetItems().Count - 1) / _itemCountPerPage)
            {
                _currentPage++;
            }
        }
    }
}