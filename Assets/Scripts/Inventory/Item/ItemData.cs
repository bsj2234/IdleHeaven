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

        public string ItemName => _itemName;
        public GameObject ItemPrefab => _itemPrefab;
        public string Description => _description;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewUsableItemData", menuName = "Item/UsableItemData")]
    public class UsableItemData : ItemData
    {
        [SerializeField] private ICharacterEffector[] _effects;
        [SerializeField] private IRequirement _useRequirement;

        public ICharacterEffector[] Effects => _effects;
        public IRequirement UseRequirement => _useRequirement;
    }

    [System.Serializable]
    public abstract class EquipmentData : ItemData
    {
        [SerializeField] private ICharacterEffector[] _effects;
        [SerializeField] private IRequirement _equipRequirement;

        public ICharacterEffector[] Effects => _effects;
        public IRequirement EquipRequirement => _equipRequirement;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "Item/WeaponData")]
    public class WeaponData : EquipmentData
    {
        [SerializeField] private string _weaponType;
        [SerializeField] private string _attackSpeed;
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;

        public string WeaponType => _weaponType;
        public string AttackSpeed => _attackSpeed;
        public int MinDamage => _minDamage;
        public int MaxDamage => _maxDamage;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewArmorData", menuName = "Item/ArmorData")]
    public class ArmorData : EquipmentData
    {
        [SerializeField] private int _defenseValue;

        public int DefenseValue => _defenseValue;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NewAmuletData", menuName = "Item/AmuletData")]
    public class AmuletData : EquipmentData
    {
        [SerializeField] private int _resistanceValue;

        public int ResistanceValue => _resistanceValue;
    }
}