using UnityEngine;

namespace IdleHeaven
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Item/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;


        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public GameObject ItemPrefab
        {
            get { return _itemPrefab; }
            set { _itemPrefab = value; }
        }

        public string PrefabPath
        {
            get
            {                 
                if (_itemPrefab == null)
                    return string.Empty;

                return _itemPrefab.name;
            }
            set
            {
                GameObject prefab = ResourceLoader.LoadPrefab(value);
                _itemPrefab = prefab;
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
}