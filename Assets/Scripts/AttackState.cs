using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    float attackCooldown = 0f;

    Transform target;
    Health targetCombat;
    AICharacterController owner;
    Attack _attack;
    public AttackState(StateMachine stateMachine) : base(stateMachine)
    {
        owner = stateMachine.Owner.GetComponent<AICharacterController>();
        _attack = stateMachine.GetComponent<Attack>();
    }

    public AttackState SetTarget(Transform target)
    {
        this.target = target;
        targetCombat = target.GetComponent<Health>();
        Debug.Assert(targetCombat != null, $"{owner.name} is Not Attackable Object");
        return this;
    }

    public override void EnterState()
    {
        Debug.Log($"Enter {this.GetType()}");
        
    }

    public override void ExitState()
    {
        Debug.Log(message: $"Exit {this.GetType()}");
    }
    public override void UpdateState()
    {
        if(targetCombat.IsDead() || target == null)
        {
            owner.RemoveEnemy(targetCombat);
            Health nextEnemy = owner.GetNearestEnemy();
            if (nextEnemy!= null)
            {
                stateMachine
                    .GetState<ChaseState>()
                    .SetTarget(nextEnemy.transform)
                    .ChangeStateTo<ChaseState>();
            }
            else
            {
                stateMachine.ChangeState<IdleState>();
            }
        }
        bool isAttackable = attackCooldown < 0f;
        if (isAttackable)
        {
            Debug.Log($"{owner} attacked {target}");
            Health enemyCombat = target.GetComponent<Health>();
            Debug.Assert(enemyCombat != null, $"Enemy is null while {owner.transform.name} try attacking");
            _attack.DealDamage(target.GetComponent<Health>(), 30f);
            attackCooldown = 1f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
