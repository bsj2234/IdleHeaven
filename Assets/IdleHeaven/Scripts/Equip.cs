using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    [SerializeField] Equipments _equipments;
    [SerializeField] ItemView _itemView;
    public void EquipItem()
    {
        EquipmentItem equipment = _itemView.ItemViewModel.Item as EquipmentItem;
        if(equipment != null)
        {
            _equipments.Equip(equipment);
        }    
        else 
        {
            Debug.Log("Item is not equipment");
        }
    }
}
