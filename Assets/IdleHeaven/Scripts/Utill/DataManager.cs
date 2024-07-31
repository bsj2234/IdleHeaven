using IdleHeaven;
using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    public int Level;
    public float Experience;
    public InventoryData Inventory;
    public EquipmentsData Equipments;
    public StageaveData Stage;
}
[Serializable]
public class InventoryData
{
    public List<Item> Items;
    public List<EquipmentItem> EquipmentItems;
    public List<Currency> Currencies;
}

[Serializable]
public class EquipmentsData
{
    public List<EquipmentItem> Equipments;
}

[Serializable]
public class StageaveData
{
    public string StageName;
    public int CurrentWaveIndex;

}


public class DataManager : MonoSingleton<DataManager>
{
    public PlayerData SaveData;
    
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CurrencyInventory _currencyInventory;
    [SerializeField] private RarityData[] _rarityDatas;
    [SerializeField] private Equipments _equipment;
    [SerializeField] private Stage _stage;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SaveData = new PlayerData();
        SaveData.Inventory = new InventoryData();
        SaveData.Inventory.Items = new List<Item>();
        SaveData.Inventory.EquipmentItems = new List<EquipmentItem>();
        SaveData.Equipments = new EquipmentsData();
        SaveData.Equipments.Equipments = new List<EquipmentItem>();
        SaveData.Stage = new StageaveData();
    }

    public void SaveGameData(PlayerData playerData, CharacterStats playerStats, Inventory inventory
        , Equipments equipments, CurrencyInventory currencyInventory, Stage stage)
    {
        Init();

        SaveData.Level = playerStats.LevelSystem.Level;
        SaveData.Experience = playerStats.LevelSystem.Exp;


        foreach (var item in inventory.Items)
        {
            if (item is EquipmentItem equipmentItem)
            {
                SaveData.Inventory.EquipmentItems.Add(equipmentItem);
            }
            else
            {
                SaveData.Inventory.Items.Add(item);
            }
        }


        SaveData.Inventory.Currencies = currencyInventory.currencies;

        SaveData.Equipments.Equipments = equipments.EquippedItems;

        SaveData.Stage.CurrentWaveIndex = stage.CurrentWaveIndex;
        SaveData.Stage.StageName = stage.StageName;

        SaveSystem.SaveData(SaveData, "pd");

    }
    public void SaveGameData()
    {
        SaveGameData(SaveData, _characterStats, _inventory, _equipment, _currencyInventory, _stage);
    }

    public void LoadData(PlayerData playerData, CharacterStats playerStats, Inventory inventory
        , Equipments equipments, CurrencyInventory currencyInventory, Stage stage)
    {
        equipments.Clear();
        inventory.Clear();
        currencyInventory.Clear();

        playerStats.LevelSystem.Level = playerData.Level;
        playerStats.LevelSystem.Exp = playerData.Experience;

        inventory.LoadItems(playerData.Inventory.Items);
        inventory.LoadEquipmentItems(playerData.Inventory.EquipmentItems);

        currencyInventory.currencies = playerData.Inventory.Currencies;

        equipments.LoadEquipments(playerData.Equipments.Equipments, inventory);

        stage.CurrentWaveIndex = playerData.Stage.CurrentWaveIndex;
        stage.StageName = playerData.Stage.StageName;

        stage.SetStageData(CSVParser.GetStageData(stage.StageName, stage.CurrentWaveIndex));
    }

    public void LoadData()
    {
        SaveData = SaveSystem.LoadData<PlayerData>("pd");
        LoadData(SaveData, _characterStats, _inventory
            , _equipment, _currencyInventory, _stage);
    }

    public RarityData GetRarityData(Rarity itemRarity)
    {
        return _rarityDatas[(int)itemRarity];
    }
}
