using System;
using System.Collections.Generic;
using IdleHeaven;

public class InventoryViewModel : IViewModel
{
    public List<IViewModel> ItemViewModels { get; private set; } = new List<IViewModel>();

    public InventoryViewModel(Inventory inventory)
    {
        foreach (var item in inventory.GetAllItems())
        {
            AddItemViewModel(item);
        }
    }

    private void AddItemViewModel(Item item)
    {
        var itemType = item.GetType();
        var viewModelType = typeof(ItemViewModel<>).MakeGenericType(itemType);
        var viewModel = (IViewModel)Activator.CreateInstance(viewModelType, item);
        ItemViewModels.Add(viewModel);
    }

    public void Refresh()
    {
        foreach (var viewModel in ItemViewModels)
        {
            viewModel.Refresh();
        }
    }
}
