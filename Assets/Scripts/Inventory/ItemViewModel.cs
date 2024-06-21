using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

public class ItemViewModel : INotifyPropertyChanged
{
    private Item _item;

    public Item Item
    {
        get => _item;
        set
        {
            if (_item == value)
            {
                return;
            }
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


    public event PropertyChangedEventHandler PropertyChanged;

    public ItemViewModel()
    {
    }

    private void OnItemChanged()
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this.Item, new PropertyChangedEventArgs(nameof(this.Item)));
        }
    }
}
