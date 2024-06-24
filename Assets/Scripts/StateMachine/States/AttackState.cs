using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    float attackCooldown = 0f;

    Transform target;
    Health targetCombat;
    CharacterAIController _controller;
    Attack _attack;
    Detector _detector;
    CharacterStats _stats;

    bool _hasStat = false;


    public AttackState(StateMachine stateMachine, CharacterAIController controller, Attack attack ,Detector detector) : base(stateMachine)
    {
        _controller = controller;
        _attack = attack;
        _detector = detector;
        _hasStat = _stats.TryGetComponent(out CharacterStats stats); 
        _stats = stats;
    }

    public AttackState SetTarget(Transform target)
    {
        this.target = target;
        targetCombat = target.GetComponent<Health>();
        Debug.Assert(targetCombat != null, $"{_controller.name} is Not Attackable Object");
        return this;
    }

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
    }
    public override void UpdateState()
    {
        bool isDestroyedOrDead = targetCombat == null || targetCombat.IsDead();
        if (isDestroyedOrDead)
        {
            bool isDestroyedAndDead = targetCombat != null && targetCombat.IsDead();
            if (isDestroyedAndDead)
            {
                _detector.RemoveTarget(targetCombat.transform);
            }

            Transform nearTarget = _detector.GetNearestTarget();
            Health nextEnemy = null;
            if(nearTarget != null)
            {
                nextEnemy = _detector.GetNearestTarget().GetComponent<Health>();
            }
            else
            {
                stateMachine.ChangeState<IdleState>();
            }


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
            Debug.Assert(targetCombat != null, $"Enemy is null while {_controller.transform.name} try attacking");
            float damage = _hasStat ? _stats.GetDamage() : 5f;
            _attack.DealDamage(target.GetComponent<Health>(),  damage);
            attackCooldown = 1f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
