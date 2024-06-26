using IdleHeaven;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Attack:MonoBehaviour
{
    public Action OnAttackSucceeded;
    public Action OnKillEnemy;
    public UnityEvent<Health, float, AttackType> OnAttack;

    public void DealDamage(Health target, float damage, AttackType attackType = AttackType.None)
    {
        bool isAttackSucceeded = target.TakeDamage(this, damage, attackType);
        if (isAttackSucceeded)
        {
            SpawnDamageUi(target.transform, damage);
            if (OnAttackSucceeded != null)
            {
                OnAttackSucceeded.Invoke();
            }
            if(target.IsDead())
            {
                if(OnKillEnemy != null)
                {
                    OnKillEnemy.Invoke();
                }
            }
            //return false;
        }
        //return true;
    }

    public void TriggerAttack(Health targetCombat, float damage, AttackType attackType)
    {
        OnAttack?.Invoke(targetCombat, damage, attackType);
    }

    private void SpawnDamageUi(Transform target, float damage)
    {
        if (target.CompareTag("Player"))
        {
            DamageUIManager.Instance.ShowDamage(target.transform, damage, Color.black);
        }
        else
        {
            DamageUIManager.Instance.ShowDamage(target.transform, damage, Color.red);
        }
    }
}