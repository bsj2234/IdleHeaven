using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterController : MonoBehaviour
{
    [SerializeField] bool AutoComponentSet = false;
    [SerializeField] Health _health;
    [SerializeField] Attack _attack;
    private Weapon weapon;

    [SerializeField] NavMeshAgent Agent_movement;

    private StateMachine stateMachine;
    public Transform[] patrolWaypoints;
    public Transform chaseTarget;
    public event Action<Transform> EnemyFoundHandler;
    private List<Health> attackAbleEnemies = new List<Health>();


    private void OnValidate()
    {
        if(AutoComponentSet)
        {
            AutoInit();
        }
    }
    private void AutoInit()
    {
        stateMachine = GetComponent<StateMachine>();
        _health = GetComponent<Health>();
    }
    //IsGrounded(Walk,Run)	IsFlying	IsSwimming	IsClimbing	IsJumping IsBurrowing	IsSliding	IsSwinging	IsCreeping	IsRolling	IsFloating	IsGliding	IsSoaring
    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Init(transform);

        var idleState = new IdleState(stateMachine);
        var patrolState = new PatrolState(stateMachine, patrolWaypoints);
        var chaseState = new ChaseState(stateMachine, chaseTarget);
        var attackState = new AttackState(stateMachine);
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
    private void OnFoundEnemy(Health target)
    {

        attackAbleEnemies.Add(target);
        Debug.Log($"Enemy found{target.transform.name}");

        if (stateMachine.CurrentState != stateMachine.GetState<AttackState>())
        {
            stateMachine.GetState<ChaseState>().SetTarget(GetNearestEnemy().GetTransform());
            stateMachine.ChangeState<ChaseState>();
        }

        if (EnemyFoundHandler != null)
        {
            EnemyFoundHandler.Invoke(GetNearestEnemy().GetTransform());
        }
        else
        {
            Debug.Log("Enemy found but handler is null");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{name} Trigger Enter");
        if(other.CompareTag("Enemy"))
        {
            OnFoundEnemy(other.transform.GetComponent<Health>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(TryGetComponent(out Health enemyCombat))
            {
                if(!_health.IsDead())
                {
                    OnLooseEnemy(enemyCombat);
                }
            }
        }
    }
    private void OnLooseEnemy(Health enemyCombat)
    {
        attackAbleEnemies.Remove(enemyCombat);
    }
    public Health GetNearestEnemy()
    {
        if (attackAbleEnemies.Count == 0)
        {
            return null;
        }
        attackAbleEnemies.Sort(SortEnemy);
        attackAbleEnemies.RemoveAll((item) => item.gameObject == null);
        if (attackAbleEnemies.Count == 0)
        {
            return null;
        }
        string Enemy_array = "";
        foreach(var enemy in attackAbleEnemies)
        {
            Enemy_array = $"{Enemy_array} ,{enemy.transform.name}";
        }
        Debug.Log(Enemy_array);

        int i = attackAbleEnemies.Count - 1;
        var result = attackAbleEnemies[i];
        while(result.IsDead())
        {
            attackAbleEnemies.RemoveAt(i--);
            result = attackAbleEnemies[i];
        }
        return result;
    }

    private int SortEnemy(Health lhs, Health rhs)
    {
        if(lhs == null)
        {
            return -1;
        }
        if(rhs == null)
        {
            return 1;
        }
        float DistanceToLhs = Vector3.Distance(lhs.transform.position,transform.position);
        float DistanceToRhs = Vector3.Distance(rhs.transform.position,transform.position);
        return DistanceToRhs.CompareTo(DistanceToLhs);
    }

    public void RemoveEnemy(Health enemy)
    {
        if (attackAbleEnemies.Count == 0)
        {
            Debug.Log("Tried pop but empty");
            return;
        }
        attackAbleEnemies.Remove(enemy);
    }
    public float GetDistance(Vector3 origin)
    {
        return Vector3.Distance(origin, transform.position);
    }
    public void KillNearlestEnemy()
    {
        Health nearestEnmey = GetNearestEnemy();
        if (nearestEnmey == null)
        {
            return;
        }
        _attack.DealDamage(nearestEnmey,99999);
    }
}