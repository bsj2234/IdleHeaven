using System;
using UnityEngine;

namespace IdleHeaven
{
    public class CharacterStats : MonoBehaviour
    {
        public Stats Stats;
        public LevelSystem LevelSystem;


        private Stats _effectBonusStats = new Stats();
        private Stats _equipmentBonusStats = new Stats();


        private Equipments _equipments;



        private void Awake()
        {
            if (TryGetComponent(out Equipments equipments))
            {
                _equipments = equipments;
                _equipments.OnEquipped += OnEquippedHandler;
                _equipments.OnUnEquipped += OnUnequippedHandler;
            }
        }
        private void OnDestroy()
        {
            if(_equipments != null)
            {
                _equipments.OnEquipped -= OnEquippedHandler;
                _equipments.OnUnEquipped -= OnUnequippedHandler;
            }
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



        //========장착, 해제이벤트 핸들러
        private void OnEquippedHandler(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            Stats.AddStats(item.GetStatBonus());
            Stats.AddStats(item.GetEffectStatBonus());
        }

        private void OnUnequippedHandler(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            Stats.SubtractStats(item.GetStatBonus());
            Stats.SubtractStats(item.GetEffectStatBonus());
        }
        private void OnLevelUpHandler(Stats stats)
        {
            Stats.AddStat(StatType.Hp,LevelSystem.Level * 100f);
            Stats.AddStat(StatType.Attack,LevelSystem.Level * 10f);
            Stats.AddStat(StatType.Defense, LevelSystem.Level * 10f);
            Stats.AddStat(StatType.Resistance,LevelSystem.Level * 10f);
        }

        public float GetDamage()
        {
            float rand = UnityEngine.Random.Range(0f, 1f);

            if(rand >= Stats[StatType.CritChance])
            {
                Debug.Log("Critical Hit");
                Debug.Log(Stats[StatType.Attack] * Stats[StatType.CritDamage]);
               return Stats[StatType.Attack] * Stats[StatType.CritChance];
            }
            else
            {
                Debug.Log("Normal Hit");
                Debug.Log(Stats[StatType.Attack]);
               return Stats[StatType.Attack];
            }
        }
    }
}