using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public string Name { get; set; }
    public ItemData ItemData{ get; set;}
    public Sprite Icon { get; set;}


    public ItemInfo(string name, Sprite icon)
    {
        Name = name;
        Icon = icon;
    }
}

public class WeaponItem : ItemInfo
{
    public int damage;

    public WeaponItem (string name ,Sprite icon,int damage) : base(name, icon)
    {
        this.damage = damage;
    }
}
public class EquipementItem : ItemInfo
{
    public int _defense;

    public EquipementItem(string name, Sprite icon, int damage) : base(name, icon)
    {
        this._defense = damage;
    }
}

public class ItemData
{
    private GameObject ItemPrefab;
    private string Desctription;
}
