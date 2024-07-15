using IdleHeaven;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAIController : MonoBehaviour
{
    [SerializeField] bool AutoComponentSet = false;
    [SerializeField] Health _health;
    [SerializeField] Attack _attack;
    [SerializeField] Detector _detector;

    [SerializeField] NavMeshAgent NavAgent_movement;

    [SerializeField] Transform[] _patrolWaypoints;
    [SerializeField] Transform _chaseTarget;

    private StateMachine stateMachine;

    private void OnValidate()
    {
        if(AutoComponentSet)
        {
            AutoInit();
            AutoComponentSet = false;
        }
    }
    private void AutoInit()
    {
        stateMachine = GetComponent<StateMachine>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _detector = GetComponentInChildren<Detector>();
    }
    //IsGrounded(Walk,Run)	IsFlying	IsSwimming	IsClimbing	IsJumping IsBurrowing	IsSliding	IsSwinging	IsCreeping	IsRolling	IsFloating	IsGliding	IsSoaring
    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Init(transform);

        var idleState = new IdleState(stateMachine, _detector);
        var patrolState = new PatrolState(stateMachine, _patrolWaypoints);
        var chaseState = new ChaseState(stateMachine, _chaseTarget, _detector);
        var attackState = new AttackState(stateMachine, _attack, _detector);
        var deadState = new DeadState(stateMachine);

        stateMachine.AddState(idleState);
        stateMachine.AddState(patrolState);
        stateMachine.AddState(chaseState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(deadState);

        stateMachine.ChangeState<IdleState>();

        _health.OnDead.AddListener(ChangeStateToDead);
    }
    private void OnDestroy()
    {

        _health.OnDead.RemoveListener(ChangeStateToDead);
    }
    public void ChangeToAuto()
    {
        stateMachine.ChangeState<AttackState>();
    }

    public void KillNearlestEnemy()
    {
        Transform nearestEnmey = _detector.GetNearestTarget();
        if (nearestEnmey == null)
        {
            return;
        }
        _attack.DealDamage(nearestEnmey.GetComponent<Health>(),99999);
    }
    private void ChangeStateToDead(Attack attack, Health health)
    {
        stateMachine.ChangeState<DeadState>();
    }
}