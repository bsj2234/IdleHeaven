

using UnityEngine;

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