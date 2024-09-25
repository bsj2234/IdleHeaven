using System;
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
        public LevelSystem LevelSystem;
        public Stats ResultStats = new Stats();
        private Stats _effectBonusStats = new Stats();
        private Stats _equipmentBonusStats = new Stats();
        protected Stats _baseStats = new Stats();

        private Health _health;

        private Equipments _equipments;


        private void Awake()
        {
            if (TryGetComponent(out Equipments equipments))
            {
                _equipments = equipments;
                _equipments.OnEquipmentsChagned += RefreshEquipmentStats;
            }
            _health = GetComponent<Health>();
            CalcResultStats();
            _health.SetMaxHp(ResultStats[StatType.Hp]);
            _health.ResetHpWithRatio(1);
            ResultStats.stats[(int)StatType.Hp].StatChanged += (Stat stat) =>
            {
                _health.SetMaxHp(stat.Value);
            };
        }
        private void OnEnable()
        {
        }
        private void OnDestroy()
        {
            if (_equipments != null)
            {
                _equipments.OnEquipmentsChagned -= RefreshEquipmentStats;
            }
        }

        internal void Init(Stats baseStats, int level)
        {
            SetBaseStat(baseStats);
            LevelSystem.Level = level;
            CalcResultStats();
        }

        public Stats GetResultStats()
        {
            return ResultStats;
        }

        protected virtual void CalcResultStats()
        {
            ResultStats.Clear();
            ResultStats.AddStats(_baseStats);

            ResultStats[StatType.Hp] = ResultStats[StatType.Hp] + (LevelSystem.Level) * 100f;
            ResultStats[StatType.Attack] = ResultStats[StatType.Attack] + (LevelSystem.Level) * 15f;
            ResultStats[StatType.Defense] = ResultStats[StatType.Defense] + (LevelSystem.Level) * 15f;

            ResultStats.AddStats(_effectBonusStats);
            ResultStats.AddStats(_equipmentBonusStats);

            ResultStats[StatType.Speed] = Mathf.Min( 500f ,10f + ResultStats[StatType.Speed]);
            ResultStats[StatType.AttackSpeed] = Mathf.Min( 15f ,1f + ResultStats[StatType.AttackSpeed]);
        }

        public void RefreshEquipmentStats(EquipmentType type, Item item, Equipments equipments)
        {
            _equipmentBonusStats.Clear();
            foreach (EquipmentItem equipment in equipments.GetEquippedItems().Values)
            {
                _equipmentBonusStats.AddStats(equipment.ResultStats);
            }
            CalcResultStats();
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
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand <= ResultStats[StatType.CritChance])
            {
                return new DamageInfo
                {
                    Damage = ResultStats[StatType.Attack] * ( 1f + ResultStats[StatType.CritDamage]),
                    AttackType = AttackType.ChargedMelee
                };

            }
            else
            {
                Debug.Log("Normal Hit");
                Debug.Log(ResultStats[StatType.Attack]);
                return new DamageInfo
                {
                    Damage = ResultStats[StatType.Attack],
                    AttackType = AttackType.Melee
                };
            }
        }

        public float GetCharacterBattleRating()
        {
            float result = 0f;
            float damage = ResultStats[StatType.Attack];
            float criticalMulti = (1f + ResultStats[StatType.CritDamage]) * ResultStats[StatType.CritChance];
            result = ResultStats[StatType.AttackSpeed] * criticalMulti * damage;
            return result;
        }

        public void SetBaseStat(Stats baseStats)
        {
            _baseStats = baseStats;
            CalcResultStats();
        }


        //========이벤트 핸들러
        public void OnLevelUpHandler()
        {
            CalcResultStats();
        }

    }
}