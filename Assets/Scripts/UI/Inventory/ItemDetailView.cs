using IdleHeaven;
using UnityEngine;

public class ItemDetailView : MonoBehaviour
{
    [SerializeField] ItemView _itemView;

    public UIWindow Window;
    public void Init(Item item)
    {
        _itemView.SetItem(item);
    }
}
