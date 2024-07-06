using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IdleHeaven
{
    public interface IRandomItemInstance
    {
        Item GetRandomItemInstance(string name, GenerateInfo generateInfo);
    }

    public struct GenerateInfo
    {
        public int EnemyLevel;
        public Rarity ItemRarity;

        public GenerateInfo(int enemyLevel, Rarity rarity)
        {
            EnemyLevel = enemyLevel;
            ItemRarity = rarity;
        }
    }
    [System.Serializable]
    public struct RarityTable
    {
        [Range(0f, 1f)] public float Common;
        [Range(0f, 1f)] public float Uncommon;
        [Range(0f, 1f)] public float Epic;
        [Range(0f, 1f)] public float Unique;
        [Range(0f, 1f)] public float Legendary;

        public RarityTable(float common, float uncommon, float epic, float unique, float legendary)
        {
            Common = common;
            Uncommon = uncommon;
            Epic = epic;
            Unique = unique;
            Legendary = legendary;
        }

    }
    public enum ItemType
    {
        Weapon,
        Equipment,
        Usable
    }


    [System.Serializable]
    public class ItemGenerator
    {
        [SerializeField] RarityTable _rarityTable;

        [SerializeField] List<string> weaponDatas;
        [SerializeField] List<string> accessoryDatas;
        [SerializeField] List<string> equipmentDatas;


        public void Init()
        {
            if (weaponDatas == null)
            {
                weaponDatas = CSVParser.Instance.GetItems(ItemType.Weapon).Keys.ToList();
            }
            if (equipmentDatas == null)
            {
                equipmentDatas = CSVParser.Instance.GetItems(ItemType.Equipment).Keys.ToList();
            }
        }
        public void Init(in RarityTable rarityTable)
        {
            _rarityTable = rarityTable;
            Init();
        }


        public Item GenerateItem(GenerateInfo info, string name = "")
        {
            Rarity rarity = GetRandomRairity(_rarityTable);

            if (rarity == Rarity.Common)
            {
                return new Gold(1000);
            }

            info.ItemRarity = rarity;

            ItemType itemType = GetRandomEnum<ItemType>();
            Dictionary<string, ItemData> randomItemDatas = CSVParser.Instance.GetItems(itemType);
            ItemData randomItemdata = GetRandomFromDictionary(randomItemDatas);

            if (string.IsNullOrEmpty(name))
                name = randomItemdata.ItemName;

            Item randomItem = RandomItem(name, info, randomItemdata);
            return randomItem;
        }





        private Item RandomItem(string itemName, GenerateInfo generateInfo, IRandomItemInstance itemData)
        {
            return itemData.GetRandomItemInstance(itemName, generateInfo);
        }


        private Rarity GetRandomRairity(RarityTable rarityTable)
        {
            float randVal = Random.Range(0f, 1f);
            if (randVal < rarityTable.Legendary)
            {
                return Rarity.Legendary;
            }
            if (randVal < rarityTable.Epic)
            {
                return Rarity.Epic;
            }
            if (randVal < rarityTable.Uncommon)
            {
                return Rarity.Uncommon;
            }
            if (randVal < rarityTable.Common)
            {
                return Rarity.Common;
            }
            Debug.Assert(false, "Error Not a possible situation");
            return Rarity.Error;
        }
        private T GetRandomEnum<T>() where T : System.Enum
        {
            System.Array values = System.Enum.GetValues(typeof(T));
            int itemType = Random.Range(0, values.Length);
            return (T)values.GetValue(itemType);

        }
        private T GetRandomFromDictionary<T>(Dictionary<string, T> dictionary)
        {
            var item = dictionary.Values.GetEnumerator();


            int randomIndex = Random.Range(0, dictionary.Count);

            for (int i = 0; i < randomIndex + 1; i++)
            {
                item.MoveNext();
            }

            return item.Current;
        }
    }
}
