using IdleHeaven;
using UnityEngine;


[System.Serializable]
public class Currency : Item
{
    public Currency()
    {
    }

    public Currency(int quantity) : base(quantity)
    {
    }

    public Currency(string name, CurrencyType type, int quantity = 1) : base(name, new ItemData(), quantity)
    {
    }

    public CurrencyType CurrencyType { get; internal set; }
}

[System.Serializable]
public class Gold : Currency
{
    public Gold(int amount) : base ("Gold", CurrencyType.Gold, amount)
    {
    }
}
