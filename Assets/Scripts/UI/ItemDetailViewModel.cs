using IdleHeaven;
using UnityEngine;

public class ItemDetailViewModel : MonoBehaviour
{
    [SerializeField] CurrencyInventory _currencyInventory;
    public bool TryEnhanceItem(EquipmentItem equipment)
    {
        if (equipment is EquipmentItem)
        {
            if(equipment.TryEnhance(_currencyInventory))
            {
                Debug.Log("Enhanced");
                return true;
            }
            else
            {
                Debug.Log("Not enough gold");
                return false;
            }
        }
        Debug.Log("Item is Not Equipment");
        return false;
    }
}
