//using UnityEngine;

//namespace IdleHeaven
//{
//    public class ScriptableObjectCreator : MonoBehaviour
//    {
//        // Example method to create and initialize a WeaponData instance
//        public WeaponData CreateWeaponData(string weaponName, string weaponType, string attackSpeed, int minDamage, int maxDamage)
//        {
//            WeaponData weaponData = ScriptableObject.CreateInstance<WeaponData>();
//            weaponData.name = weaponName;
//            weaponData.ItemName = weaponName;
//            weaponData.Description = $"{weaponName} Description";
//            weaponData.WeaponType = weaponType;
//            weaponData.AttackSpeed = attackSpeed;
//            weaponData.MinDamage = minDamage;
//            weaponData.MaxDamage = maxDamage;

//            return weaponData;
//        }

//        // Example method to create and initialize an ArmorData instance
//        public ArmorData CreateArmorData(string armorName, int defenseValue)
//        {
//            ArmorData armorData = ScriptableObject.CreateInstance<ArmorData>();
//            armorData.name = armorName;
//            armorData.ItemName = armorName;
//            armorData.Description = $"{armorName} Description";
//            armorData.DefenseValue = defenseValue;

//            return armorData;
//        }

//        // Save the ScriptableObject instance as an asset
//        public void SaveScriptableObjectAsAsset(ScriptableObject obj, string path)
//        {
//#if UNITY_EDITOR
//            UnityEditor.AssetDatabase.CreateAsset(obj, path);
//            UnityEditor.AssetDatabase.SaveAssets();
//#endif
//        }

//        // Example usage
//        private void Start()
//        {
//            WeaponData newWeaponData = CreateWeaponData("Excalibur", "Sword", "Fast", 50, 70);
//            SaveScriptableObjectAsAsset(newWeaponData, "Assets/Data/Excalibur.asset");

//            ArmorData newArmorData = CreateArmorData("Dragon Armor", 100);
//            SaveScriptableObjectAsAsset(newArmorData, "Assets/Data/DragonArmor.asset");
//        }
//    }
//}