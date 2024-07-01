using IdleHeaven;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    public int Level;
    public float Experience;
    public InventoryData Inventory;
}
[Serializable]
public class InventoryData
{
    public List<Item> Items;
    public List<Currency> Currencies;
}


public class DataManager : MonoSingleton<DataManager>
{
    public PlayerData PlayerData = new PlayerData();



    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CurrencyInventory _currencyInventory;


    private void Start()
    {
        PlayerData.Inventory = new InventoryData();
    }

    public void SavePlayerData(CharacterStats characterStats, Inventory inventory, CurrencyInventory currencyInventory)
    {
        PlayerData.Level = characterStats.LevelSystem.Level;
        PlayerData.Experience = characterStats.LevelSystem.Exp;
        PlayerData.Inventory.Items = inventory.Items;
        PlayerData.Inventory.Currencies = currencyInventory.currencies;

        SaveSystem.SaveData(PlayerData, "pd");

    }
    public void SavePlayerData()
    {
        PlayerData.Level = _characterStats.LevelSystem.Level;
        PlayerData.Experience = _characterStats.LevelSystem.Exp;
        PlayerData.Inventory.Items = _inventory.Items;
        PlayerData.Inventory.Currencies = _currencyInventory.currencies;

        SaveSystem.SaveData(PlayerData, "pd");

    }

}
