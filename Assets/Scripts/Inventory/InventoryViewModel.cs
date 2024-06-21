using System.ComponentModel;

namespace IdleHeaven
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private Inventory _inventory;

        public ItemViewModel[] itemViewModels;

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

        public InventoryViewModel(Inventory inventory)
        {
            Inventory = inventory;
            itemViewModels = new ItemViewModel[Inventory.INVENTORY_SIZE];

            for (int i = 0; i < Inventory.INVENTORY_SIZE; i++)
            {
                itemViewModels[i] = new ItemViewModel();
            }

            _inventory.OnInventoryChanged += HandleInventoryChanged;

        }

        private void HandleInventoryChanged(Item item)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(Inventory, new PropertyChangedEventArgs(nameof(Inventory)));
            }
        }

        protected void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Inventory))
            {
                for (int i = 0; i < _inventory.INVENTORY_SIZE; i++)
                {
                    Item currentItem = _inventory.Items[i];
                    if (currentItem != null)
                    {
                        itemViewModels[i].Item = currentItem;
                    }
                }
            }
        }

    }
}