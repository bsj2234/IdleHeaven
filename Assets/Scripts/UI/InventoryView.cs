using UnityEngine;

public class InventoryView : MonoBehaviour, IView
{
    [SerializeField] Transform _contentPanel;
    [SerializeField] GameObject _itemViewPrefab;
    [SerializeField] Inventory _inventory;
    [SerializeField] ItemView[] _items;
    private InventoryViewModel _viewModel;

    private void Start()
    {
        _viewModel = new InventoryViewModel(_inventory);
        Initialize(_viewModel);
    }

    public void Initialize(IViewModel viewModel)
    {
        this._viewModel = (InventoryViewModel)viewModel;
        Refresh();
    }

    public void Refresh()
    {
        //foreach (Transform child in _contentPanel)
        //{
        //    Destroy(child.gameObject);
        //}
        DrawItems();
    }

    private void DrawItems()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            //var itemViewObject = Instantiate(_itemViewPrefab, _contentPanel);

            //var itemView = itemViewObject.GetComponent<IView>();
            //itemView.Initialize(itemViewModel);
            ItemView itemViewObject = _items[i];
            if(i < _viewModel.ItemViewModels.Count)
            {
                itemViewObject.Initialize(_viewModel.ItemViewModels[i]);
            }
            else
            {
                itemViewObject = null;
            }
        }
    }
}
