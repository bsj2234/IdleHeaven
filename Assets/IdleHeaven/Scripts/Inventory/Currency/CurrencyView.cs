using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private CurrencyInventory _currencyInventory;

    [SerializeField] private TMPro.TMP_Text Text_Currency;
    [SerializeField] private TMPro.TMP_Text Text_Diamond;

    private void Start()
    {
        _currencyInventory.OnCurrencyChanged += CurrencyInventory_OnCurrencyChanged;
        UpdateCurrencyView(_currencyInventory);
    }
    private void OnDisable()
    {
        _currencyInventory.OnCurrencyChanged -= CurrencyInventory_OnCurrencyChanged;
    }

    private void CurrencyInventory_OnCurrencyChanged(object sender, System.EventArgs e)
    {
        UpdateCurrencyView(sender as CurrencyInventory);
    }

    private void UpdateCurrencyView(CurrencyInventory currency)
    {
        if (currency != null)
        {
            Text_Currency.text = currency.currencies[(int)CurrencyType.Gold].Quantity.ToString("N0");
            Text_Diamond.text = currency.currencies[(int)CurrencyType.Diamond].Quantity.ToString("N0");
            return;
        }
        
    }
}
