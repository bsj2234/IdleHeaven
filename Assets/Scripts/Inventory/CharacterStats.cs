using System.Collections.Generic;
using UnityEngine;
using IdleHeaven;

namespace IdleHeaven
{
    public class CharacterStats : MonoBehaviour
    {
        public Stats Stats;

        private Stats _effectBonusStats = new Stats();
        private Stats _equipmentBonusStats = new Stats();

        private Equipments equipments;

        private void Awake()
        {
            equipments = GetComponent<Equipments>();
        }

        public float GetStatValue(StatType statType)
        {
            return Stats[statType];
        }

        public void RefreshEquipmentStats(Equipments equipments)
        {
            _equipmentBonusStats.Clear();
            foreach (EquipmentItem equipment in equipments.GetEquippedItems().Values)
            {
                _equipmentBonusStats.AddStats(equipment.GetStatBonus());
            }
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
    }
}