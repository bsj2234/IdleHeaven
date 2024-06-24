using IdleHeaven;
using UnityEngine;

public class ItemDetailView : MonoBehaviour
{
    [SerializeField] ItemView _itemView;
    [SerializeField] UIButtonHoldDetector _button;
    [SerializeField] ItemDetailViewModel _itemViewModel;

    public UIWindow Window;

    public void OnOpen(Item item)
    {
        _itemView.SetItem(item);
        _button.onClick.AddListener(() => OnClick(item));
    }

    public void OnClick(Item item)
    {
        if (item is EquipmentItem)
        {
            if (_itemViewModel.TryEnhanceItem(item as EquipmentItem))
            {
                Debug.Log("Enhance Succesded");
            }
        }
    }
}
