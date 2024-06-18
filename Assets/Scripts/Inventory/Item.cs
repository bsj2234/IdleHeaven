using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IdleHeaven
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] ItemInfo ItemInfo;

        public void Init(ItemInfo info)
        {
            ItemInfo = info;
        }

        public GameObject GetPrefab()
        {
            return ItemInfo.ItemData.ItemPrefab;
        }
    }
}