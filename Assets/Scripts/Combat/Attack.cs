using System;
using UnityEngine;

[System.Serializable]
public class Attack:MonoBehaviour
{
    public Action OnAttackSucceeded;
    public bool DealDamage(Health target, float damage)
    {
        bool isAttackSucceeded = target.TakeDamage(this, damage);
        if (isAttackSucceeded)
        {
            if(OnAttackSucceeded != null)
            {
                OnAttackSucceeded.Invoke();
            }
            return false;
        }
        return true;
    }
}