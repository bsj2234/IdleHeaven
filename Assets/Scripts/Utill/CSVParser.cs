using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace IdleHeaven
{
    public class CSVParser : MonoBehaviour
    {
        public string csvFilePath;

        private void Start()
        {
            List<Item> items = ParseCSV(csvFilePath);
            foreach (var item in items)
            {
                Debug.Log($"Parsed item: {item.Name}");
            }
        }

        public List<Item> ParseCSV(string filePath)
        {
            List<Item> items = new List<Item>();

            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] fields = lines[i].Split(',');

                string itemName = fields[1];
                string itemType = fields[2];
                string description = fields[3];
                string prefabPath = fields[4];

                if (itemType == "Weapon")
                {
                    string weaponType = fields[5];
                    string attackSpeed = fields[6];
                    int minDamage = int.Parse(fields[7]);
                    int maxDamage = int.Parse(fields[8]);
                    WeaponData weaponData = ScriptableObject.CreateInstance<WeaponData>();
                    weaponData.name = itemName;
                    weaponData.Description = description;
                    weaponData.PrefabPath = prefabPath;
                    weaponData.WeaponType = weaponType;
                    weaponData.AttackSpeed = attackSpeed;
                    weaponData.MinDamage = minDamage;
                    weaponData.MaxDamage = maxDamage;
                    EquipmentItem weaponItem = new EquipmentItem(itemName, weaponData);
                    items.Add(weaponItem);
                }
                else if (itemType == "Armor")
                {
                    int defenseValue = int.Parse(fields[9]);
                    ArmorData armorData = ScriptableObject.CreateInstance<ArmorData>();
                    armorData.name = itemName;
                    armorData.Description = description;
                    armorData.PrefabPath = prefabPath;
                    armorData.DefenseValue = defenseValue;
                    EquipmentItem armorItem = new EquipmentItem(itemName, armorData);
                    items.Add(armorItem);
                }
                else if (itemType == "Usable")
                {
                    string effectType = fields[10];
                    int effectValue = int.Parse(fields[11]);
                    UsableItemData usableItemData = ScriptableObject.CreateInstance<UsableItemData>();
                    usableItemData.name = itemName;
                    usableItemData.Description = description;
                    usableItemData.PrefabPath = prefabPath;
                    // Add effects to the usable item data as needed
                    items.Add(new Item(itemName, usableItemData)); // Adjust as necessary
                }
            }

            return items;
        }
    }
}
