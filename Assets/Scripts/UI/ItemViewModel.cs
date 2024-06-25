using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

public class ItemViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private Item _item;

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
