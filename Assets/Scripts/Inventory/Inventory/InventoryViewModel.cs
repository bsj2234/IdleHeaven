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
                foreach(Item item in items)
                {
                    if(item is EquipmentItem)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            else
            {
                return _inventory.GetItems();
            }
        }

    }
}