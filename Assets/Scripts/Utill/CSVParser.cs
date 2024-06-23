using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace IdleHeaven
{
    public class CSVParser : MonoSingleton<CSVParser>
    {
        public string csvItemFilePath;
        public string csvEffectFilePath;
        public List<Item> items;
        public List<ItemEffectData> effects;

        [SerializeField] bool Launch=false;

        private void OnValidate()
        {
            if(Launch)
            {
                Launch = false;
                items = ParseCSV<Item>(csvItemFilePath);
                foreach (var item in items)
                {
                    Debug.Log($"Parsed item: {item.Name}");
                }
                effects = ParseCSV<ItemEffectData>(csvEffectFilePath);
                foreach (var effect in effects)
                {
                    Debug.Log($"Parsed effect: {effect.Stat} with Rarity: {effect.Rarity}");
                }
            }
        }

        public List<T> ParseCSV<T>(string filePath) where T : new()
        {
            List<T> records = new List<T>();

            string[] lines = File.ReadAllLines(filePath);
            string[] headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] fields = lines[i].Split(',');
                T record = new T();

                for (int j = 0; j < headers.Length; j++)
                {
                    string header = headers[j];
                    string field = fields[j];

                    if (typeof(T) == typeof(Item))
                    {
                        PopulateItemRecord(record as Item, headers, fields);
                    }
                    else if (typeof(T) == typeof(ItemEffectData))
                    {
                        PopulateItemEffectRecord(record as ItemEffectData, header, field);
                    }
                }

                records.Add(record);
            }

            return records;
        }

        private void PopulateItemRecord(Item item, string[] headers, string[] fields)
        {
            string itemType = fields[Array.IndexOf(headers, "Type")];
            string itemName = fields[Array.IndexOf(headers, "Name")];
            string description = fields[Array.IndexOf(headers, "Description")];
            string prefabPath = fields[Array.IndexOf(headers, "PrefabPath")];

            if (itemType == "Weapon")
            {
                WeaponData weaponData = ScriptableObject.CreateInstance<WeaponData>();
                weaponData.name = itemName;
                weaponData.Description = description;
                weaponData.PrefabPath = prefabPath;
                weaponData.WeaponType = fields[Array.IndexOf(headers, "WeaponType")];
                weaponData.AttackSpeed = fields[Array.IndexOf(headers, "AttackSpeed")];
                weaponData.MinDamage = int.Parse(fields[Array.IndexOf(headers, "MinDamage")]);
                weaponData.MaxDamage = int.Parse(fields[Array.IndexOf(headers, "MaxDamage")]);
                item.ItemData = weaponData;
                SaveScriptableObject(weaponData, $"Assets/Resources/Weapons/{itemName}.asset");
            }
            else if (itemType == "Armor")
            {
                ArmorData armorData = ScriptableObject.CreateInstance<ArmorData>();
                armorData.name = itemName;
                armorData.Description = description;
                armorData.PrefabPath = prefabPath;
                armorData.DefenseValue = int.Parse(fields[Array.IndexOf(headers, "DefenseValue")]);
                item.ItemData = armorData;
                SaveScriptableObject(armorData, $"Assets/Resources/Armors/{itemName}.asset");
            }
            else if (itemType == "Usable")
            {
                UsableItemData usableItemData = ScriptableObject.CreateInstance<UsableItemData>();
                usableItemData.name = itemName;
                usableItemData.Description = description;
                usableItemData.PrefabPath = prefabPath;
                //usableItemData.EffectType = fields[Array.IndexOf(headers, "EffectType")];
                //usableItemData.EffectValue = int.Parse(fields[Array.IndexOf(headers, "EffectValue")]);
                item.ItemData = usableItemData; 
                SaveScriptableObject(usableItemData, $"Assets/Resources/Usables/{itemName}.asset");
            }
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
                    effect.MinValue = int.Parse(field);
                    break;
                case "MaxValue":
                    effect.MaxValue = int.Parse(field);
                    break;
            }
        }



        private void SaveScriptableObject(ScriptableObject scriptableObject, string path)
        {
            // Create the asset
            AssetDatabase.CreateAsset(scriptableObject, path);
            // Save the asset
            AssetDatabase.SaveAssets();
            // Refresh the AssetDatabase
            AssetDatabase.Refresh();
        }
    }
}
