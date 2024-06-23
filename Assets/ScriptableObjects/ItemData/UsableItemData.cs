using IdleHeaven;
using UnityEngine;

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