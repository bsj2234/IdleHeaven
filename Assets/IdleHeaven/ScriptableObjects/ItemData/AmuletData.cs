using UnityEngine;

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