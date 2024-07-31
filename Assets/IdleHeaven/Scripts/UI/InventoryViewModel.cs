using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public List<Item> GetItemList(string filter, string sortType, bool descending)
        {
            List<Item> items = Inventory.GetItems();
            List<Item> filteredItemList = GetFilteredItemList(items, filter);
            List<Item> sortedItemList = GetSortedItemList(filteredItemList, sortType, descending);
            List<Item> pagedItemList = GetPagedItemList(sortedItemList, _currentPage, _itemCountPerPage);
            return pagedItemList;
        }

        private List<Item> GetFilteredItemList(List<Item> items, string filter)
        {
            if (items.Count == 0)
            {
                return items;
            }
            if (filter == "all")
                return items;

            List<Item> filteredItemList = null;

            if (filter == "equipment")
                filteredItemList = items.Where(item => item is EquipmentItem).ToList();
            if (filter == "weapon")
                filteredItemList = items.Where(item => item is EquipmentItem equipmentItem &&
                equipmentItem.EquipmentData.ItemType == ItemType.Weapon).ToList();
            if (filter == "armor")
                filteredItemList = items.Where(item => item is EquipmentItem equipmentItem &&
                equipmentItem.EquipmentData.ItemType == ItemType.Armor).ToList();
            if (filter == "usable")
                filteredItemList = items.Where(item => item.ItemData.ItemType == ItemType.Usable).ToList();
            if (filter == "zoroLevel")
            {
                filteredItemList = items.Where(item => item is EquipmentItem equipmentItem &&
                equipmentItem.Level == 1).ToList();
            }
            if (filteredItemList == null)
                throw new System.Exception("Invalid filter type");
            else
                return filteredItemList;
        }

        private List<Item> GetSortedItemList(List<Item> items, string sortType, bool descending)
        {
            if (items.Count == 0)
            {
                return items;
            }
            if (sortType == "none")
            {
                return items;
            }
            List<Item> result = new List<Item>(items);
            if (sortType == "attack")
            {
                result.Sort(SortByAttack);
            }
            if (sortType == "defense")
            {
                result.Sort(SortByDefense);
            }
            if (sortType == "rarity")
            {
                result.Sort(SortByRarity);
            }
            if (sortType == "name")
            {
                result.Sort(SortByName);
            }
            if (sortType == "type")
            {
                result.Sort(SortByType);
            }
            if (sortType == "critChance")
            {
                result.Sort(SortByCritChance);
            }
            if (sortType == "critDamage")
            {
                result.Sort(SortByCritDamage);
            }
            if (sortType == "level")
            {
                result.Sort(SortByLevel);
            }
            if (sortType == "attackSpeed")
            {
                result.Sort(SortByAttack);
            }
            if (!descending)
            {
                result.Reverse();
            }
            return result;
        }
        private List<Item> GetPagedItemList(List<Item> items, int page, int countPerPage)
        {
            List<Item> result = new List<Item>();
            for (int i = page * countPerPage; i < (page + 1) * countPerPage; i++)
            {
                if (i < items.Count)
                {
                    result.Add(items[i]);
                }
            }
            return result;
        }

        #region Sort Methods
        private int SortByLevel(Item a, Item b)
        {
            if (a is EquipmentItem && b is EquipmentItem)
            {
                EquipmentItem equipmentItemA = a as EquipmentItem;
                EquipmentItem equipmentItemB = b as EquipmentItem;
                int result = equipmentItemA.Level.CompareTo(equipmentItemB.Level);
                if (result == 0)
                {
                    return equipmentItemA.CurrentIndex.CompareTo(equipmentItemB.CurrentIndex);
                }
                else
                {
                    return result;
                }
            }
            return 0;
        }
        private int SortByAttack(Item a, Item b)
        {
            if (a is EquipmentItem && b is EquipmentItem)
            {
                EquipmentItem equipmentItemA = a as EquipmentItem;
                EquipmentItem equipmentItemB = b as EquipmentItem;
                float attackA = equipmentItemA.ResultStats[StatType.Attack];
                float attackB = equipmentItemB.ResultStats[StatType.Attack];
                int result = attackA.CompareTo(attackB);


                if (result == 0)
                {
                    return equipmentItemA.CurrentIndex.CompareTo(equipmentItemB.CurrentIndex);
                }
                else
                {
                    return result;
                }
            }
            return a.CurrentIndex.CompareTo(b.CurrentIndex);
        }
        private int SortByDefense(Item a, Item b)
        {
            if (a is EquipmentItem && b is EquipmentItem)
            {
                EquipmentItem equipmentItemA = a as EquipmentItem;
                EquipmentItem equipmentItemB = b as EquipmentItem;
                float defenseA = equipmentItemA.ResultStats[StatType.Defense];
                float defenseB = equipmentItemB.ResultStats[StatType.Defense];
                int result = defenseA.CompareTo(defenseB);
                if (result == 0)
                {
                    return equipmentItemA.CurrentIndex.CompareTo(equipmentItemB.CurrentIndex);
                }
                else
                {
                    return result;
                }
            }
            return a.CurrentIndex.CompareTo(b.CurrentIndex);
        }
        private int SortByRarity(Item a, Item b)
        {
            if (a is EquipmentItem && b is EquipmentItem)
            {
                EquipmentItem equipmentItemA = a as EquipmentItem;
                EquipmentItem equipmentItemB = b as EquipmentItem;
                int result = equipmentItemA.RarityData.Rarity.CompareTo(equipmentItemB.RarityData.Rarity);
                if (result == 0)
                {
                    return equipmentItemA.CurrentIndex.CompareTo(equipmentItemB.CurrentIndex);
                }
                else
                {
                    return result;
                }
            }
            return a.CurrentIndex.CompareTo(b.CurrentIndex);
        }
        private int SortByName(Item a, Item b)
        {
            int result = a.Name.CompareTo(b.Name);
            if (result == 0)
            {
                return a.CurrentIndex.CompareTo(b.CurrentIndex);
            }
            else
            {
                return result;
            }
        }
        private int SortByType(Item a, Item b)
        {
            int result = a.ItemData.ItemType.CompareTo(b.ItemData.ItemType);
            if (result == 0)
            {
                return a.CurrentIndex.CompareTo(b.CurrentIndex);
            }
            else
            {
                return result;
            }
        }
        private int SortByCritChance(Item a, Item b)
        {
            if (a is EquipmentItem && b is EquipmentItem)
            {
                EquipmentItem equipmentItemA = a as EquipmentItem;
                EquipmentItem equipmentItemB = b as EquipmentItem;
                float critChanceA = equipmentItemA.ResultStats[StatType.CritChance];
                float critChanceB = equipmentItemB.ResultStats[StatType.CritChance];
                int result = critChanceA.CompareTo(critChanceB);
                if (result == 0)
                {
                    return equipmentItemA.CurrentIndex.CompareTo(equipmentItemB.CurrentIndex);
                }
                else
                {
                    return result;
                }
            }
            return a.CurrentIndex.CompareTo(b.CurrentIndex);
        }
        private int SortByCritDamage(Item a, Item b)
        {

            if (a is EquipmentItem && b is EquipmentItem)
            {
                EquipmentItem equipmentItemA = a as EquipmentItem;
                EquipmentItem equipmentItemB = b as EquipmentItem;
                float critDamageA = equipmentItemA.ResultStats[StatType.CritDamage];
                float critDamageB = equipmentItemB.ResultStats[StatType.CritDamage];
                int result = critDamageA.CompareTo(critDamageB);
                if (result == 0)
                {
                    return equipmentItemA.CurrentIndex.CompareTo(equipmentItemB.CurrentIndex);
                }
                else
                {
                    return result;
                }
            }
            return a.CurrentIndex.CompareTo(b.CurrentIndex);
        }
        #endregion
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