using UnityEngine;
using IdleHeaven;

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
        ItemType = ItemType.Equipment;
    }

    public float DefenseValue
    {
        get { return _stats[StatType.Defense]; }
        set { _stats[StatType.Defense] = value; }
    }

    public override Item GetRandomItemInstance(string name, GenerateInfo generateInfo)
    {
        EquipmentItem equipment = new EquipmentItem(name, this, generateInfo.EnemyLevel);
        equipment.BaseStats[StatType.Defense] = this.DefenseValue;
        float min = RarityData.GetRarityData(generateInfo.ItemRarity).MinBaseStatMulti;
        float max = RarityData.GetRarityData(generateInfo.ItemRarity).MaxBaseStatMulti;
        equipment.BaseStats[StatType.Defense] *= Random.Range(min, max);
        equipment.SetRandomEffects();
        return equipment;
    }
}
