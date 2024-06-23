using System;
using System.ComponentModel;

namespace IdleHeaven
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        private Equipments _equipments;

        public ItemViewModel[] itemViewModels;

        public event PropertyChangedEventHandler PropertyChanged;
        public int slotsCount = Enum.GetNames(typeof(EquipmentSlot)).Length;

        public Equipments Equipments
        {
            get => _equipments;
            set
            {
                _equipments = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Equipments)));
                }
            }
        }

        public EquipmentViewModel(Equipments equipments)
        {
            Equipments = equipments;
            itemViewModels = new ItemViewModel[slotsCount];

            for (int i = 0; i < slotsCount; i++)
            {
                itemViewModels[i] = new ItemViewModel();
            }

            _equipments.OnEquipmentsChagned += HandleEquipmentsChagned;

        }

        public Item GetItem(EquipmentSlot slot)
        {
            return _equipments.GetEquippedItem(slot);
        }

        private void HandleEquipmentsChagned(EquipmentSlot slot, Item item)
        {
            itemViewModels[(int)slot].Item = item;

            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(Equipments, new PropertyChangedEventArgs(nameof(Equipments)));
            }
        }
    }
}