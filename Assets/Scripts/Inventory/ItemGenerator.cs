using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{

    public struct GenerateInfo
    {
        public int EnemyLevel;

        public GenerateInfo(int enemyLevel)
        {
            EnemyLevel = enemyLevel;
        }
    }
    [System.Serializable]
    public struct RarityTable
    {
        [Range(0f, 1f)] public float Common;
        [Range(0f, 1f)] public float Epic;
        [Range(0f, 1f)] public float Unique;
        [Range(0f, 1f)] public float Legendary;
    }
    public enum Rairity
    {
        Common,
        Epic,
        Unique,
        Legendary,
        Error
    }
    public enum ItemType
    {
        Weapon,
        Equipment,
        Accessory
    }
    public enum WeaponType
    {
        Sword,
        Axe,
        Spear
    }
    public enum EquipmentType
    {
        Helmet,
        Chestplate,
        Gauntlets,
        Greaves,
        Shield
    }
    public enum AccessoryType
    {
        Ring,
        Amulet,
        Belt
    }


    [System.Serializable]
    public class ItemGenerator
    {
        [SerializeField] RarityTable _rarityTable;

        private Dictionary<string, ItemData> itemDatas = new Dictionary<string, ItemData>();
        [SerializeField] List<ItemData> weaponDatas = new List<ItemData>();
        [SerializeField] List<ItemData> accessoryDatas = new List<ItemData>();
        [SerializeField] List<ItemData> equipmentDatas = new List<ItemData>();

        public Item GenerateItem(GenerateInfo info)
        {
            ItemType randomType = RandomKind<ItemType>();
            ItemData data;
            switch (randomType)
            {
                case ItemType.Weapon:
                    data = GetRandomWeapon();
                    break;
                case ItemType.Equipment:
                    data = GetRandomEquipment();
                    break;
                case ItemType.Accessory:
                    data = GetRandomAccessory();
                    break;
                default:
                    Debug.Assert(false);
                    return null;
            }
            Item item = new Item($"{data.ItemPrefab.name}",data);
            return item;


        }

        private ItemData GetRandomAccessory()
        {
            return accessoryDatas[Random.Range(0, accessoryDatas.Count)];
        }

        private ItemData GetRandomEquipment()
        {
            return equipmentDatas[Random.Range(0, equipmentDatas.Count)];
        }

        private ItemData GetRandomWeapon()
        {
            return weaponDatas[Random.Range(0, weaponDatas.Count)];
        }

        private Rairity RandomRairity()
        {
            Random.InitState((int)Time.time);
            float randVal = Random.Range(0f, 1f);
            if (randVal < _rarityTable.Legendary)
            {
                return Rairity.Legendary;
            }
            if (randVal < _rarityTable.Unique)
            {
                return Rairity.Unique;
            }
            if (randVal < _rarityTable.Epic)
            {
                return Rairity.Epic;
            }
            if (randVal < _rarityTable.Common)
            {
                return Rairity.Common;
            }
            Debug.Assert(false, "Error Not a possible situation");
            return Rairity.Error;
        }
        private T RandomKind<T>() where T : System.Enum
        {
            Random.InitState((int)(Time.realtimeSinceStartup * 1000f));
            System.Array values = System.Enum.GetValues(typeof(T));
            int itemType = Random.Range(0, values.Length);
            return (T)values.GetValue(itemType);

        }

    }
}
