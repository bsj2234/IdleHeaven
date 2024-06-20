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

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewUsableItemData", menuName = "Item/UsableItemData")]
    public class UsableItemData : ItemData
    {
        [SerializeField] private ICharacterEffector[] _effects;
        [SerializeField] private IRequirement _useRequirement;

        public ICharacterEffector[] Effects
        {
            get { return _effects; }
            set { _effects = value; }
        }

        public IRequirement UseRequirement
        {
            get { return _useRequirement; }
            set { _useRequirement = value; }
        }
    }

    [System.Serializable]
    public abstract class EquipmentData : ItemData
    {
        [SerializeField] private ICharacterEffector[] _effects;
        [SerializeField] private IRequirement _equipRequirement;

        public ICharacterEffector[] Effects
        {
            get { return _effects; }
            set { _effects = value; }
        }

        public IRequirement EquipRequirement
        {
            get { return _equipRequirement; }
            set { _equipRequirement = value; }
        }
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "Item/WeaponData")]
    public class WeaponData : EquipmentData
    {
        [SerializeField] private string _weaponType;
        [SerializeField] private string _attackSpeed;
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;

        public string WeaponType
        {
            get { return _weaponType; }
            set { _weaponType = value; }
        }

        public string AttackSpeed
        {
            get { return _attackSpeed; }
            set { _attackSpeed = value; }
        }

        public int MinDamage
        {
            get { return _minDamage; }
            set { _minDamage = value; }
        }

        public int MaxDamage
        {
            get { return _maxDamage; }
            set { _maxDamage = value; }
        }
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewArmorData", menuName = "Item/ArmorData")]
    public class ArmorData : EquipmentData
    {
        [SerializeField] private int _defenseValue;

        public int DefenseValue
        {
            get { return _defenseValue; }
            set { _defenseValue = value; }
        }
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewAmuletData", menuName = "Item/AmuletData")]
    public class AmuletData : EquipmentData
    {
        [SerializeField] private int _resistanceValue;

        public int ResistanceValue
        {
            get { return _resistanceValue; }
            set { _resistanceValue = value; }
        }
    }
}


