using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold
}

public class CurrencyInventory : MonoBehaviour
{
    public List<Currency> currencies = new List<Currency>();

    public bool TryUseGold(int amount)
    {
        if (currencies[(int)CurrencyType.Gold].Amount < amount)
        {
            return false;
        }

        currencies[(int)CurrencyType.Gold].Amount -= amount;
        return true;
    }

    private void Start()
    {
        currencies.Add(new Gold(100000));
    }
}
