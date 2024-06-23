using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private Equipments _equipments;
    private void Awake()
    {
        _equipments.OnEquipped += OnEquipped;
        _equipments.OnUnEquipped += OnUnequipped;
    }

    private void OnDestroy()
    {
        _equipments.OnEquipped -= OnEquipped;
        _equipments.OnUnEquipped -= OnUnequipped;
    }

    private void OnEquipped(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
    {
        _characterStats.Stats.AddStats(item.GetStatBonus());
        _characterStats.Stats.AddStats(item.GetEffectStatBonus());
    }

    private void OnUnequipped(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
    {
        _characterStats.Stats.SubtractStats(item.GetStatBonus());
        _characterStats.Stats.SubtractStats(item.GetEffectStatBonus());
    }
}
