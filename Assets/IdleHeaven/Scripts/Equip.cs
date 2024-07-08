using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    [SerializeField] Equipments _equipments;
    [SerializeField] ItemView _itemView;
    public bool EquipItem(Item item)
    {
        EquipmentItem equipment = item as EquipmentItem;
        if(equipment != null)
        {
            _equipments.Equip(equipment);
            return true;
        }    
        else
        {
            Debug.Log("Item is not equipment");
            return false;
        }
    }
}
