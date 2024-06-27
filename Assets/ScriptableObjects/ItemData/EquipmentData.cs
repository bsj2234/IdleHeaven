using UnityEngine;
using IdleHeaven;

[System.Serializable]
public class EquipmentData: ItemData
{
    [SerializeField] EquipmentType _equipmentSlot;
    [SerializeField] float _defanseValue;
    [SerializeField] Stats _stats;

    public EquipmentType EquipmentSlot
    {
        get { return _equipmentSlot; }
        set { _equipmentSlot = value; }
    }

    public float DefenseValue
    {
        get { return _defanseValue; }
        set { _defanseValue = value; }
    }
}
