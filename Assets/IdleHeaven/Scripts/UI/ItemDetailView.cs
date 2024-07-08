using IdleHeaven;
using UnityEngine;

public class ItemDetailView : MonoBehaviour
{
    [SerializeField] ItemView _itemView;
    [SerializeField] UIButtonHoldDetector _equipButton;
    [SerializeField] UIButtonHoldDetector _enhanceButton;
    [SerializeField] ItemDetailViewModel _itemDetailViewModel;
    [SerializeField] Equip _equip;

    public UIClosedWindow Window;

    public void Init(Item item)
    {
        _itemView.SetItem(item);
        if (_equipButton != null)
        {
            _equipButton.onClick.AddListener(OnClickEquip);
        }
        if (_enhanceButton != null)
        {
            _enhanceButton.onClick.AddListener(OnClickEnhance);
        }
    }
    public void OnDisable()
    {
        if (_equipButton != null)
        {
            _equipButton.onClick.RemoveListener(OnClickEquip);
        }
        if(_enhanceButton != null)
        {
            _enhanceButton.onClick.RemoveListener(OnClickEnhance);
        }
    }

    public void OnClickEquip()
    {
        Item item = _itemView.ItemViewModel.Item;
        if (item is EquipmentItem)
        {
            if (_equip.EquipItem(item))
            {
                Debug.Log("Equip Succese");
            }
        }
    }
    public void OnClickEnhance()
    {
        Item item = _itemView.ItemViewModel.Item;
        if (item is EquipmentItem equipmentItem)
        {
            if (_itemDetailViewModel.TryEnhanceItem(equipmentItem))
            {
                Debug.Log("Enhance Succese");
            }
        }
    }
}
