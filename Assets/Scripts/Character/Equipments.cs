using IdleHeaven;
using System;
using UnityEngine;

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield }

public class Equipments : MonoBehaviour
{
    public EquipmentItem[] slot;
    public CharacterStats statBonuses;

    public Action<EquipmentItem> OnEquipped;
    public Action<EquipmentItem> OnUnEquipped;

    private void Awake()
    {
        slot = new EquipmentItem[System.Enum.GetValues(typeof(EquipmentSlot)).Length;
    }

    public void Equip(EquipmentItem item)
    {
        OnEquipped.Invoke(item);
    }

    public void Unequip(EquipmentItem item)
    {
        OnUnEquipped.Invoke(item);
    }
}
