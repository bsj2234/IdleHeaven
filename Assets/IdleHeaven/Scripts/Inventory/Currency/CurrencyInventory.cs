using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold,
    Diamond
}

public class CurrencyInventory : MonoBehaviour
{
    public List<Currency> currencies = new List<Currency>();

    public bool TryUseGold(int amount)
    {
        if (currencies[(int)CurrencyType.Gold].Quantity < amount)
        {
            return false;
        }

        currencies[(int)CurrencyType.Gold].Quantity -= amount;
        return true;
    }

    public void AddCurency(Currency currency)
    {
        currencies[(int)currency.CurrencyType].Quantity += currency.Quantity;
    }

    private void Start()
    {
        currencies.Add(new Gold(100000));
    }
}
