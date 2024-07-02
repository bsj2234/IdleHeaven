using UnityEngine;

namespace IdleHeaven
{
    public struct DamageInfo
    {
        public AttackType AttackType;
        public float Damage;
    }

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
                LevelSystem.OnLevelUp.AddListener(OnLevelUpHandler);
            }
        }
        private void OnDestroy()
        {
            if (_equipments != null)
            {
                _equipments.OnEquipped -= OnEquippedHandler;
                _equipments.OnUnEquipped -= OnUnequippedHandler;
                LevelSystem.OnLevelUp.RemoveListener(OnLevelUpHandler);
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
                _equipmentBonusStats.AddStats(equipment.ResultStats);
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

        public DamageInfo GetDamage()
        {
            float rand = UnityEngine.Random.Range(0f, 100f);
            if (rand <= Stats[StatType.CritChance])
            {
                Debug.Log("Critical Hit");
                Debug.Log(Stats[StatType.Attack] * Stats[StatType.CritDamage]);
                return new DamageInfo
                {
                    Damage = Stats[StatType.Attack] * Stats[StatType.CritDamage],
                    AttackType = AttackType.ChargedMelee
                };

            }
            else
            {
                Debug.Log("Normal Hit");
                Debug.Log(Stats[StatType.Attack]);
                return new DamageInfo
                {
                    Damage = Stats[StatType.Attack],
                    AttackType = AttackType.Melee
                };
            }
        }



        //========장착, 해제이벤트 핸들러
        private void OnEquippedHandler(Equipments equipments, EquipmentType slot, EquipmentItem item)
        {
            Stats.AddStats(item.ResultStats);
        }

        private void OnUnequippedHandler(Equipments equipments, EquipmentType slot, EquipmentItem item)
        {
            Stats.SubtractStats(item.ResultStats);
        }
        private void OnLevelUpHandler()
        {
            Stats.AddStat(StatType.Hp, LevelSystem.Level * 100f);
            Stats.AddStat(StatType.Attack, LevelSystem.Level * 10f);
            Stats.AddStat(StatType.Defense, LevelSystem.Level * 10f);
        }
    }
}