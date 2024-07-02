using System;
using System.Collections.Generic;
using System.IO;
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
        public string csvEnemiesFilePath;
        private Dictionary<string, ItemData> WeaponDatas = new Dictionary<string, ItemData>();
        public Dictionary<string, ItemData> EquipmentDatas = new Dictionary<string, ItemData>();
        public Dictionary<string, ItemData> UsableDatas = new Dictionary<string, ItemData>();
        public Dictionary<string, ItemEffectData> effects = new Dictionary<string, ItemEffectData>();
        public Dictionary<string, EnemyData> enemies = new Dictionary<string, EnemyData>();

        [SerializeField] private PlayerData _playerData;

        [SerializeField] bool Launch = false;

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
            effects = ParseCSV<ItemEffectData>(csvEffectFilePath);
            foreach (var effect in effects)
            {
                Debug.Log($"Parsed effect: {effect.Value.Stat} with Rarity: {effect.Value.Rarity}");
            }
            enemies = ParseCSV<EnemyData>(csvEnemiesFilePath);
            foreach (var enemy in enemies)
            {
                Debug.Log($"Parsed enemy: {enemy.Value.Name}");
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
                    case ItemType.Equipment:
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
                case ItemType.Equipment:
                    return EquipmentDatas;
                case ItemType.Usable:
                    return UsableDatas;
                default:
                    return null;
            }
        }
    }
}
