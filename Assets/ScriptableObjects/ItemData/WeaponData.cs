using UnityEngine;

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
