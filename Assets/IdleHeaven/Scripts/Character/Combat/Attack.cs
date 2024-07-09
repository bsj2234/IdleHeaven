using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IdleHeaven
{
    public class Attack : MonoBehaviour
    {
        public Action OnAttackSucceeded;
        public Action OnKillEnemy;
        public UnityEvent<Health, float, AttackType> OnAttack;

        public void DealDamage(Health target, float damage, AttackType attackType = AttackType.None)
        {
            if(target == null)
            {
               return;
            }

            Stats targetStats = target.GetComponent<CharacterStats>()?.GetResultStats();

            bool isAttackSucceeded = target.TakeDamage(this, damage, attackType,targetStats);
            if (isAttackSucceeded)
            {
                if (OnAttackSucceeded != null)
                {
                    OnAttackSucceeded.Invoke();
                }
                if (target.IsDead())
                {
                    if (OnKillEnemy != null)
                    {
                        OnKillEnemy.Invoke();
                    }
                }
                //return false;
            }
            //return true;
        }

        public void RangeAttack(Health target, float damage, Detector detector, float distance, float angle, AttackType attackType = AttackType.None)
        {
            if(transform.GetComponent<Health>().IsDead())
            {
                return;
            }

            List<Transform> targets = detector.GetSortedEnemys();
            if(targets == null)
            {
                return;
            }

            for (int i = 0; i < targets.Count; i++)
            {
                if(targets[i] == null)
                {
                    continue;
                }
                Transform item = targets[i];
                if (Vector3.Distance(item.transform.position, transform.position) > distance)
                {
                    break;
                }
                if(angle * 0.5f < Vector3.Angle(transform.forward, item.transform.position - transform.position))
                {
                    continue;
                }
                DealDamage(item.GetComponent<Health>(), damage, attackType);
            }
        }

        public void TriggerAttack(Health targetCombat, float damage, AttackType attackType)
        {
            OnAttack?.Invoke(targetCombat, damage, attackType);
        }
    }
}
