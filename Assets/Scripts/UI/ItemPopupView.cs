using IdleHeaven;
using System;
using UnityEngine;

public class ItemPopupView : MonoBehaviour
{
    [SerializeField] ItemViewModel _itemViewModel;

    public UIWindow Window;

    public void Init(ItemView itemView)
    {
        _itemViewModel = itemView.ItemViewModel;
    }
}
