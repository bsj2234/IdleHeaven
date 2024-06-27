using UnityEngine;

namespace IdleHeaven
{
    //SpecificType	AttackSpeed	MinDamage	MaxDamage	DefenseValue
    [System.Serializable]
    public class ItemData : IRandomItemInstance
    {
        [SerializeField] string _itemName;
        [SerializeField] ItemType _itemType;
        [SerializeField] GameObject _itemPrefab;
        [SerializeField] string _description;
        [SerializeField] Sprite _icon;


        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public ItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        public GameObject ItemPrefab
        {
            get
            {
                if (_itemPrefab == null)
                    Debug.LogError($"{_itemName}ItemPrefab is null");
                return _itemPrefab;
            }
            set { _itemPrefab = value; }
        }

        public string PrefabPath
        {
            get
            {
                if (_itemPrefab == null)
                {
                    return string.Empty;
                }

                return _itemPrefab.name;
            }
            set
            {
                _itemPrefab = Resources.Load<GameObject>(value);
                if (_itemPrefab == null)
                    Debug.LogError($"{_itemName}cannot Find Prefab In Path {value}");
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Sprite Icon
        {
            get
            {
                return _icon;
            }
        }
        public string IconPath
        {
            set
            {
                _icon = Resources.Load<Sprite>(value);
            }
        }

        public virtual Item GetRandomItemInstance(string name)
        {
            return new Item(name, this);
        }
    }
}