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
    private Weapon weapon;

    [SerializeField] NavMeshAgent Agent_movement;

    private StateMachine stateMachine;
    public Transform[] patrolWaypoints;
    public Transform chaseTarget;
    public event Action<Transform> EnemyFoundHandler;



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

        var idleState = new IdleState(stateMachine, this, _detector);
        var patrolState = new PatrolState(stateMachine, patrolWaypoints);
        var chaseState = new ChaseState(stateMachine, chaseTarget, _detector);
        var attackState = new AttackState(stateMachine, this, _attack, _detector);
        var deadState = new DeadState(stateMachine);

        stateMachine.AddState(idleState);
        stateMachine.AddState(patrolState);
        stateMachine.AddState(chaseState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(deadState);

        stateMachine.ChangeState<IdleState>();
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

}