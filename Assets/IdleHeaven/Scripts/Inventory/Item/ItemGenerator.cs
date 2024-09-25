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
        [Range(0f, 1f)] public float None;
        [Range(0f, 1f)] public float Currency;
        [Range(0f, 1f)] public float Common;
        [Range(0f, 1f)] public float Uncommon;
        [Range(0f, 1f)] public float Rare;
        [Range(0f, 1f)] public float Epic;
        [Range(0f, 1f)] public float Legendary;

        public RarityTable(float none, float currency, float common,
            float uncommon, float epic, float rare, float legendary)
        {
            None = none;
            Currency = currency;
            Common = common;
            Uncommon = uncommon;
            Rare = rare;
            Epic = epic;
            Legendary = legendary;
        }

    }
    public enum ItemType
    {
        Weapon,
        Armor,
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
                equipmentDatas = CSVParser.Instance.GetItems(ItemType.Armor).Keys.ToList();
            }
        }
        public void Init(in RarityTable rarityTable)
        {
            _rarityTable = rarityTable;
            Init();
        }


        public Item GenerateItem(int levelinfo, string name = "")
        {
            Rarity rarity = GetRandomRairity(_rarityTable);

            if (rarity == Rarity.None)
            {
                return null;
            }
            if (rarity == Rarity.Currency)
            {
                return new Gold(levelinfo * 50);
            }

            ItemType itemType = GetRandomEnum<ItemType>();
            Dictionary<string, ItemData> randomItemDatas = CSVParser.Instance.GetItems(itemType);
            ItemData randomItemdata = GetRandomFromDictionary(randomItemDatas);

            if (string.IsNullOrEmpty(name))
                name = randomItemdata.ItemName;

            Item randomItem = RandomItem(name, new GenerateInfo(levelinfo, rarity), randomItemdata);
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
            if (randVal < rarityTable.Rare)
            {
                return Rarity.Rare;
            }
            if (randVal < rarityTable.Uncommon)
            {
                return Rarity.Uncommon;
            }
            if (randVal < rarityTable.Common)
            {
                return Rarity.Common;
            }
            if (randVal < rarityTable.Currency)
            {
                return Rarity.Currency;
            }
            if (randVal < rarityTable.None)
            {
                return Rarity.None;
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
