using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

public class ItemViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private Item _item;
    private EquipmentItem _equipmentItem;
    public event PropertyChangedEventHandler PropertyChanged;

    public Item Item
    {
        get => _item;
        set
        {
            if (_item == value)
            {
                return;
            }

            // Unsubscribe from the old item
            if (_item != null)
            {
                _item.OnItemChanged -= OnItemChanged;
            }
            _item = value;
            _equipmentItem = _item as EquipmentItem;
            if (_item != null)
            {
                _item.OnItemChanged += OnItemChanged;
            }


            if (PropertyChanged != null)
            {
                PropertyChanged(this.Item, new PropertyChangedEventArgs(nameof(Item)));
            }
        }
    }


    private void OnItemChanged()
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this.Item, new PropertyChangedEventArgs(nameof(this.Item)));
        }
    }
}
