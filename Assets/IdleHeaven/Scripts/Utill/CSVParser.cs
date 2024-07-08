using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace IdleHeaven
{

    public interface IKeyProvider
    {
        string GetKey();
    }

    public class CSVParser : MonoSingleton<CSVParser>
    {
        public string csvItemFilePath;
        public string csvEffectFilePath;
        public string csvEnemieFilePath;
        public string csvStageFilePath;
        public string csvItemDropFilePath;
        public string csvEnemySpawnFilePath;

        private Dictionary<string, ItemData> WeaponDatas = new Dictionary<string, ItemData>();
        public Dictionary<string, ItemData> EquipmentDatas = new Dictionary<string, ItemData>();
        public Dictionary<string, ItemData> UsableDatas = new Dictionary<string, ItemData>();

        public Dictionary<string, ItemDropTableData>  ItemDropDatas = new Dictionary<string, ItemDropTableData>();
        public Dictionary<string, EnemySpawnData>  EnemySpawnDatas = new Dictionary<string, EnemySpawnData>();
        public Dictionary<string, ItemEffectData> EffectDatas = new Dictionary<string, ItemEffectData>();
        public Dictionary<string, EnemyData> EnemyDatas = new Dictionary<string, EnemyData>();
        public Dictionary<string, StageData> StageDatas = new Dictionary<string, StageData>();

        [SerializeField] private PlayerData _playerData;

        public static StageData GetStageData(string stageName, int waveIndex)
        {
            return Instance.StageDatas[$"{stageName}-{waveIndex}"];
        }
        public static StageData GetStageData(string stageName)
        {
            return Instance.StageDatas[$"{stageName}-{0}"];
        }


        private void Awake()
        {
            ParseItemToDictionaryCSV(csvItemFilePath);
            foreach (var item in WeaponDatas)
            {
                Debug.Log($"Parsed item: {item.Value.ItemName}");
            }
            foreach (var item in EquipmentDatas)
            {
                Debug.Log($"Parsed item: {item.Value.ItemName}");
            }
            foreach (var item in UsableDatas)
            {
                Debug.Log($"Parsed item: {item.Value.ItemName}");
            }
            foreach (var item in WeaponDatas)
            {
                Debug.Log($"Parsed item: {item.Value.ItemName}");
            }
            EffectDatas = ParseCSV<ItemEffectData>(csvEffectFilePath);
            foreach (var effect in EffectDatas)
            {
                Debug.Log($"Parsed effect: {effect.Value.Stat} with Rarity: {effect.Value.Rarity}");
            }
            EnemyDatas = ParseCSV<EnemyData>(csvEnemieFilePath);
            foreach (var enemy in EnemyDatas)
            {
                Debug.Log($"Parsed enemy: {enemy.Value.Name}");
            }
            StageDatas = ParseCSV<StageData>(csvStageFilePath);
            foreach (var stage in StageDatas)
            {
                Debug.Log($"Parsed enemy: {stage.Value.GetKey()}");
            }
            ItemDropDatas = ParseCSV<ItemDropTableData>(csvItemDropFilePath);
            foreach (var itemDrop in ItemDropDatas)
            {
                Debug.Log($"Parsed itemDrop: {itemDrop.Value.Name}");
            }
            EnemySpawnDatas = ParseCSV<EnemySpawnData>(csvEnemySpawnFilePath);
            foreach (var enemySpawn in EnemySpawnDatas)
            {
                Debug.Log($"Parsed enemySpawn: {enemySpawn.Value.Name}");
            }
        }

        private void ParseItemToDictionaryCSV(string filePath)
        {
            Dictionary<string, Item> recordes = new Dictionary<string, Item>();
            TextAsset file = Resources.Load<TextAsset>(filePath);
            string[] lines = file.text.Split("\r\n");
            string[] header = lines[0].Split(',');


            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == string.Empty)
                {
                    continue;
                }
                string[] fields = lines[i].Split(',');

                ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), fields[Array.IndexOf(header, "Type")]);

                switch (itemType)
                {
                    case ItemType.Weapon:
                        WeaponData weaponData = new WeaponData();
                        weaponData.ItemName = fields[Array.IndexOf(header, "Name")];
                        weaponData.Description = fields[Array.IndexOf(header, "Description")];
                        weaponData.WeaponType = fields[Array.IndexOf(header, "SpecificType")];
                        weaponData.AttackSpeed = fields[Array.IndexOf(header, "AttackSpeed")];
                        weaponData.MinDamage = int.Parse(fields[Array.IndexOf(header, "MinDamage")]);
                        weaponData.MaxDamage = int.Parse(fields[Array.IndexOf(header, "MaxDamage")]);
                        weaponData.IconPath = fields[Array.IndexOf(header, "IconPath")];
                        WeaponDatas.Add(weaponData.ItemName, weaponData);
                        break;
                    case ItemType.Armor:
                        EquipmentData equipmentData = new EquipmentData();
                        equipmentData.ItemName = fields[Array.IndexOf(header, "Name")];
                        equipmentData.Description = fields[Array.IndexOf(header, "Description")];
                        equipmentData.DefenseValue = int.Parse(fields[Array.IndexOf(header, "DefenseValue")]);
                        equipmentData.EquipmentSlot = (EquipmentType)Enum.Parse(typeof(EquipmentType), fields[Array.IndexOf(header, "SpecificType")]);
                        equipmentData.IconPath = fields[Array.IndexOf(header, "IconPath")];
                        EquipmentDatas.Add(equipmentData.ItemName, equipmentData);
                        break;
                    case ItemType.Usable:
                        UsableItemData usableItemData = new UsableItemData();
                        usableItemData.ItemName = fields[Array.IndexOf(header, "Name")];
                        usableItemData.Description = fields[Array.IndexOf(header, "Description")];
                        usableItemData.EffectType = (EffectType)Enum.Parse(typeof(EffectType), fields[Array.IndexOf(header, "EffectType")]);
                        usableItemData.EffectValue = int.Parse(fields[Array.IndexOf(header, "EffectValue")]);
                        usableItemData.IconPath = fields[Array.IndexOf(header, "IconPath")];
                        UsableDatas.Add(usableItemData.ItemName, usableItemData);
                        break;
                }
            }

        }



        public Dictionary<string, T> ParseCSV<T>(string filePath) where T : IKeyProvider, new()
        {
            Dictionary<string, T> records = new Dictionary<string, T>();

            TextAsset file = Resources.Load<TextAsset>(filePath);
            string[] lines = file.text.Split("\r\n");
            string[] headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                if (lines[i] == string.Empty)
                {
                    continue;
                }
                string[] fields = lines[i].Split(',');
                T record = new T();

                for (int j = 0; j < headers.Length; j++)
                {
                    string header = headers[j];
                    string field = fields[j];

                    if (typeof(T) == typeof(ItemEffectData))
                    {
                        PopulateItemEffectRecord(record as ItemEffectData, header, field);
                    }
                    else if (typeof(T) == typeof(EnemyData))
                    {
                        PopulateEnemyRecord(record as EnemyData, header, field);
                    }
                    else if (typeof(T) == typeof(StageData))
                    {
                        PopulateStageRecord(record as StageData, header, field);
                    }
                    else if (typeof(T) == typeof(ItemDropTableData))
                    {
                        PopulateItemDropRecord(record as ItemDropTableData, header, field);
                    }
                    else if (typeof(T) == typeof(EnemySpawnData))
                    {
                        PopulateEnemySpawnRecord(record as EnemySpawnData, header, field);
                    }
                    else
                    {
                        Assert.IsTrue(false, $"Unknown type: {typeof(T)}");
                    }
                }

                records.Add(record.GetKey(), record);
            }

            return records;
        }

        private void PopulateItemEffectRecord(ItemEffectData effect, string header, string field)
        {
            switch (header)
            {
                case "EffectId":
                    effect.EffectId = int.Parse(field);
                    break;
                case "Stat":
                    effect.Stat = (StatType)Enum.Parse(typeof(StatType), field, true);
                    break;
                case "Rarity":
                    effect.Rarity = (Rarity)Enum.Parse(typeof(Rarity), field, true);
                    break;
                case "MinValue":
                    effect.MinValue = float.Parse(field);
                    break;
                case "MaxValue":
                    effect.MaxValue = float.Parse(field);
                    break;
                case "LevelMultiplier":
                    effect.LevelMultiplier = float.Parse(field);
                    break;
                case "MaxLevelMultiplier":
                    effect.MaxLevelMultiplier = float.Parse(field);
                    break;
            }
        }

        private void PopulateEnemyRecord(EnemyData enemy, string header, string field)
        {
            switch (header)
            {
                case "EnemyID":
                    break;
                case "EnemyType":
                    enemy.EnemyType = field;
                    break;
                case "EnemyName":
                    enemy.Name = field;
                    break;
                case "BaseHealth":
                    enemy.BaseHealth = float.Parse(field);
                    break;
                case "BaseAttack":
                    enemy.BaseAttack = float.Parse(field);
                    break;
                case "BaseSpeed":
                    enemy.BaseSpeed = float.Parse(field);
                    break;
                case "BaseDefense":
                    enemy.BaseDefense = float.Parse(field);
                    break;
                case "PrefabPath":
                    enemy.PrefabPath = field;
                    break;
                default:
                    Assert.IsTrue(false, $"Unknown field: {header}");
                    break;
            }
        }

        private void PopulateStageRecord(StageData stage, string header, string field)
        {
            switch (header)
            {
                case "ID":
                    stage.ID = int.Parse(field);
                    break;
                case "StageName":
                    stage.StageName = field;
                    break;
                case "WaveIndex":
                    stage.WaveIndex = int.Parse(field);
                    break;
                case "NextStage":
                    stage.NextStage = field;
                    break;
                case "TargetKillCount":
                    stage.TargetKillCount = int.Parse(field);
                    break;
                case "MapName":
                    stage.MapName = field;
                    break;
                case "ItemSpawner":
                    stage.ItemSpawnData = field;
                    break;
                case "EnemySpawner":
                    stage.EnemySpawnData = field;
                    break;
                case "WaveCount":
                    stage.WaveCount = int.Parse(field);
                    break;
                case "RequireStage":
                    break;
                default:
                    Assert.IsTrue(false, $"Unknown field: {header}");
                    break;

            }
        }

        private void PopulateItemDropRecord(ItemDropTableData itemDrop, string header, string field)
        {
            switch (header)
            {
                case "Name":
                    itemDrop.Name = field;
                    break;
                case "None":
                    itemDrop.None = float.Parse(field);
                    break;
                case "Currency":
                    itemDrop.Currency = float.Parse(field);
                    break;
                case "Common":
                    itemDrop.Common = float.Parse(field);
                    break;
                case "Uncommon":
                    itemDrop.Uncommon = float.Parse(field);
                    break;
                case "Rare":
                    itemDrop.Unique = float.Parse(field);
                    break;
                case "Epic":
                    itemDrop.Epic = float.Parse(field);
                    break;
                case "Legendary":
                    itemDrop.Legendary = float.Parse(field);
                    break;
                case "WeaponDatas":
                    itemDrop.WeaponDatas = field.Split(',');
                    break;
                case "AccessoryDatas":
                    itemDrop.AccessoryDatas = field.Split(',');
                    break;
                case "EquipmentDatas":
                    itemDrop.EquipmentDatas = field.Split(',');
                    break;
                default:
                    Assert.IsTrue(false, $"Unknown field: {header}");
                    break;
            }
        }

        private void PopulateEnemySpawnRecord(EnemySpawnData enemySpawn, string header, string field)
        {
            switch (header)
            {
                case "Name":
                    enemySpawn.Name = field;
                    break;
                case "Enemies":
                    enemySpawn.Enemies = field.Split(',');
                    break;
                case "MaxEnemies":
                    enemySpawn.MaxEnemies = int.Parse(field);
                    break;
                case "StageLevel":
                    enemySpawn.StageLevel = int.Parse(field);
                    break;
                case "SpawnInterval":
                    enemySpawn.SpawnInterval = float.Parse(field);
                    break;
                default:
                    Assert.IsTrue(false, $"Unknown field: {header}");
                    break;
            }
        }
        private void SaveScriptableObject(ScriptableObject scriptableObject, string path)
        {
            //unity editor only script
#if false
            // Create the asset
            AssetDatabase.CreateAsset(scriptableObject, path);
            // Save the asset
            AssetDatabase.SaveAssets();
            // Refresh the AssetDatabase
            AssetDatabase.Refresh();
#endif
        }

        public Dictionary<string, ItemData> GetItems(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    return WeaponDatas;
                case ItemType.Armor:
                    return EquipmentDatas;
                case ItemType.Usable:
                    //Todo Jin: Implement UsableDatas
                    return EquipmentDatas;
                default:
                    return null;
            }
        }
    }
}
