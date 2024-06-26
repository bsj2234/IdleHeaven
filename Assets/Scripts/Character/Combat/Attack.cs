using IdleHeaven;
using System;
using UnityEngine;

public class Attack:MonoBehaviour
{
    public Action OnAttackSucceeded;
    public Action OnKillEnemy;

    public bool DealDamage(Health target, float damage, AttackType attackType = AttackType.None)
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
            return false;
        }
        return true;
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