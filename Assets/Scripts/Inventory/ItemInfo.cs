using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public string Name { get; set; }
    public ItemData ItemData{ get; set;}
    public Sprite Icon { get; set;}


    public ItemInfo(string name, Sprite icon, ItemData data)
    {
        Name = name;
        Icon = icon;
        ItemData = data;
    }
}

public class WeaponItem : ItemInfo
{
    public int damage;

    public WeaponItem (string name ,Sprite icon, ItemData data,int damage) : base(name, icon, data)
    {
        this.damage = damage;
    }
}
public class EquipementItem : ItemInfo
{
    public int _defense;

    public EquipementItem(string name, Sprite icon, ItemData data, int defense) : base(name, icon, data)
    {
        this._defense = defense;
    }
}

[System.Serializable]
public class ItemData
{
    public GameObject ItemPrefab;
    public string Desctription;
}
