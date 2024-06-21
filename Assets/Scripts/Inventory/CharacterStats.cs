using System.Collections.Generic;
using UnityEngine;
using IdleHeaven;

namespace IdleHeaven
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private int _level;
        [SerializeField] private int _hp;


        private Stats _stats = new Stats();

        private Stats _effectBonusStats = new Stats();
        private Stats _equipmentBonusStats = new Stats();

        private Equipments equipments;

        public int Level => _level;

        private void Awake()
        {
            equipments = GetComponent<Equipments>();
            equipments.OnEquipped += OnEquipped;
        }

        public float GetStatValue(StatType statType)
        {
            return _stats[statType];
        }

        public void RefreshEquipmentStats(Equipments equipments)
        {
            _equipmentBonusStats.Clear();
            foreach (EquipmentItem equipment in equipments.GetEquippedItems().Values)
            {
                _equipmentBonusStats.AddStat(equipment.GetStatBonus());
            }
        }
        private void OnEquipped(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            RefreshEquipmentStats(equipments);
        }
        private void OnUnEquipped(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            RefreshEquipmentStats(equipments);
        }

        public void RefreshEffectStats(ICharacterEffector effector)
        {
            _effectBonusStats.Clear();
            if (effector is StatChangeEffect statChangeEffect)
            {
                statChangeEffect
                    .SetTarget(_effectBonusStats)
                    .ApplyEffect();
            }
        }
        private void OnEffectAffected(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            RefreshEquipmentStats(equipments);
        }
        private void OnEffectUnaffected(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            RefreshEquipmentStats(equipments);
        }
    }
}