using System.Collections.Generic;
using UnityEngine;

namespace IdleHeaven
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private int _level;
        [SerializeField] private int _hp;


        private Stats _stats = new Stats();
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
        private void UnEquipped(Equipments equipments, EquipmentSlot slot, EquipmentItem item)
        {
            RefreshEquipmentStats(equipments);
        }
    }
}