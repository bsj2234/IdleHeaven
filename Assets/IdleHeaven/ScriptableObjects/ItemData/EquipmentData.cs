using UnityEngine;
using IdleHeaven;
using System;

[System.Serializable]
public class EquipmentData: ItemData
{
    [SerializeField] EquipmentType _equipmentSlot;
    [SerializeField] Stats _stats = new Stats();

    public EquipmentType EquipmentSlot
    {
        get { return _equipmentSlot; }
        set { _equipmentSlot = value; }
    }

    public EquipmentData()
    {
        ItemType = ItemType.Armor;
    }

    public float DefenseValue
    {
        get { return _stats[StatType.Defense]; }
        set { _stats[StatType.Defense] = value; }
    }

    public override Item GetRandomItemInstance(string name, GenerateInfo generateInfo)
    {
        EquipmentItem item = new EquipmentItem(name, this, generateInfo.EnemyLevel);
        SetItemBaseStats(item, generateInfo);
        SetItemRarity(item, generateInfo.ItemRarity);
        item.SetRandomEffects();
        return item;
    }

    private void SetItemRarity(EquipmentItem item, Rarity itemRarity)
    {
        item.RarityData = DataManager.Instance.GetRarityData(itemRarity);
    }

    protected virtual void SetItemBaseStats(EquipmentItem item, GenerateInfo generateInfo)
    {
        item.BaseStats[StatType.Defense] = DefenseValue;
    }
}
