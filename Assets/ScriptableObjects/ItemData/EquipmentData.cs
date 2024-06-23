using UnityEngine;
using IdleHeaven;

[System.Serializable]
public abstract class EquipmentData : ItemData
{
    [SerializeField] ICharacterEffector[] _effects;
    [SerializeField] IRequirement _equipRequirement;
    [SerializeField] Stats _bonusStats;
    [SerializeField] EquipmentSlot _equipmentSlot;

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

    public Stats BonusStats
    {
        get { return _bonusStats; }
        set { _bonusStats = value; }
    }

    public EquipmentSlot EquipmentSlot
    {
        get { return _equipmentSlot; }
        set { _equipmentSlot = value; }
    }
}
