using IdleHeaven;
using UnityEngine;


public enum EffectType
{
    Speed,
    Heal,
    Damage,
    Defense
}

[System.Serializable]
public class UsableItemData : ItemData
{
    [SerializeField] private EffectType _effectType;
    [SerializeField] private float _effectValue;

    public EffectType EffectType
    {
        get { return _effectType; }
        set { _effectType = value; }
    }
    public float EffectValue
    {
        get { return _effectValue; }
        set { _effectValue = value; }
    }

}