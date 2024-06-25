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
            if(OnAttackSucceeded != null)
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
}