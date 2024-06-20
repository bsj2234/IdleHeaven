using UnityEngine;
using UnityEngine.UI;
using IdleHeaven;

public class ItemView : MonoBehaviour, IView
{
    [SerializeField] private TMPro.TMP_Text itemNameText;
    [SerializeField] private Image itemIconImage;
    private IViewModel viewModel;

    public void Initialize(IViewModel viewModel)
    {
        this.viewModel = viewModel;
        (viewModel as ItemViewModel<Item>).OnItemChanged += UpdateView;
        UpdateView();
    }

    public void Refresh()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        if (viewModel is ItemViewModel<Item> itemViewModel)
        {
            itemNameText.text = itemViewModel.ItemName;
            itemIconImage.sprite = itemViewModel.ItemIcon;
        }
    }

    private void OnDestroy()
    {
        if (viewModel is ItemViewModel<Item> itemViewModel)
        {
            itemViewModel.OnItemChanged -= UpdateView;
        }
    }
}
