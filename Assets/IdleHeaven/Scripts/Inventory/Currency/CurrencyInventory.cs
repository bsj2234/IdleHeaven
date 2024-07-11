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
    internal Action<object, EventArgs> OnCurrencyChanged;

    private void Awake()
    {
        currencies.Add(new Gold(0));
        currencies.Add(new Gold(0));
    }

    public bool TryUseGold(int amount)
    {
        if (currencies[(int)CurrencyType.Gold].Quantity < amount)
        {
            return false;
        }

        currencies[(int)CurrencyType.Gold].Quantity -= amount;
        OnCurrencyChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void AddCurency(Currency currency)
    {
        currencies[(int)currency.CurrencyType].Quantity += currency.Quantity;
        OnCurrencyChanged?.Invoke(this, EventArgs.Empty);
    }

    internal void Clear()
    {
        currencies.Clear();
    }
}
