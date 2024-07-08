using IdleHeaven;
using UnityEngine;


[System.Serializable]
public class WeaponData : EquipmentData
{
    [SerializeField] string _weaponType;
    [SerializeField] string _attackSpeed;
    [SerializeField] int _minDamage;
    [SerializeField] int _maxDamage;

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


    public WeaponData()
    {
        ItemType = ItemType.Weapon;
        EquipmentSlot = EquipmentType.Weapon;
    }

    public override Item GetRandomItemInstance(string name, GenerateInfo generateInfo)
    {
        //call the base class method
        EquipmentItem item = (EquipmentItem)base.GetRandomItemInstance(name, generateInfo);
        return item;
    }
    protected override void SetItemBaseStats(EquipmentItem item, GenerateInfo generateInfo)
    {
        item.BaseStats[StatType.Attack] = Random.Range(MinDamage, MaxDamage);
    }
}
