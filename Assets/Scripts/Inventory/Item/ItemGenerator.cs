using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public interface IRandomItemInstance
    {
        Item GetRandomItemInstance(string name);
    }

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

        [SerializeField] List<string> weaponDatas = new List<string>();
        [SerializeField] List<string> accessoryDatas = new List<string>();
        [SerializeField] List<string> equipmentDatas = new List<string>();

        public Item GenerateItem(GenerateInfo info, string name = "")
        {
            ItemType itemType = GetRandomKind<ItemType>();
            Dictionary<string, ItemData> randomItemDatas = CSVParser.Instance.GetItems(itemType);
            ItemData randomItemdata = GetRandomFromDictionary(randomItemDatas);

            if(string.IsNullOrEmpty(name))
                name = randomItemdata.ItemName;

            Item randomItem = RandomItem(name, randomItemdata);
            return randomItem;
        }



        private T GetRandomKind<T>() where T : System.Enum
        {
            Random.InitState((int)(Time.realtimeSinceStartup * 1000f));
            System.Array values = System.Enum.GetValues(typeof(T));
            int itemType = Random.Range(0, values.Length);
            return (T)values.GetValue(itemType);

        }
        private Rarity GetRandomRairity(RarityTable rarityTable)
        {
            Random.InitState((int)Time.time);
            float randVal = Random.Range(0f, 1f);
            if (randVal < rarityTable.Legendary)
            {
                return Rarity.Legendary;
            }
            if (randVal < rarityTable.Epic)
            {
                return Rarity.Epic;
            }
            if (randVal < rarityTable.Common)
            {
                return Rarity.Common;
            }
            Debug.Assert(false, "Error Not a possible situation");
            return Rarity.Error;
        }
        private Item RandomItem(string itemName, IRandomItemInstance itemData)
        {
            return itemData.GetRandomItemInstance(itemName);
        }
        private T GetRandomFromDictionary<T>(Dictionary<string, T> dictionary)
        {
            Random.InitState((int)Time.time);
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
