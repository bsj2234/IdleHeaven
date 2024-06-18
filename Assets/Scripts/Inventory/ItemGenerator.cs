using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct GenerateInfo
{
    public int EnemyLevel;
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
[System.Serializable]
public class ItemGenerator
{
    [SerializeField] RarityTable _rarityTable;
    public void GenerateItem(GenerateInfo info)
    {
        //info.EnemyLevel;
    }

    private Rairity RandomRairity()
    {
        Random.InitState((int)Time.time);
        float randVal = Random.Range(0f, 1f);
        if(randVal < _rarityTable.Legendary )
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

}
