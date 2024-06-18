using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public interface IMovable
{
}
public interface IMovableAI: IMovable
{
    public NavMeshAgent GetAgent();
}

[RequireComponent (typeof(StateMachine))]
public class AICharacterController : MonoBehaviour, IMovableAI, ICombat
{

    private Combat combat = new Combat();
    private Weapon weapon;

    [SerializeField] private NavMeshAgent Agent_movement;

    private StateMachine stateMachine;
    public Transform[] patrolWaypoints;
    public Transform chaseTarget;
    public event Action<Transform> EnemyFoundHandler;
    private List<ICombat> attackAbleEnemies = new List<ICombat>();


    //IsGrounded(Walk,Run)	IsFlying	IsSwimming	IsClimbing	IsJumping IsBurrowing	IsSliding	IsSwinging	IsCreeping	IsRolling	IsFloating	IsGliding	IsSoaring
    private void Start()
    {
        combat.Init(transform);

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
    private void OnFoundEnemy(ICombat target)
    {

        attackAbleEnemies.Add(target);
        Debug.Log($"Enemy found{target.GetTransform().name}");

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
            OnFoundEnemy(other.transform.GetComponent<ICombat>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(TryGetComponent(out ICombat enemyCombat))
            {
                if(!combat.IsDead())
                {
                    OnLooseEnemy(enemyCombat);
                }
            }
        }
    }
    private void OnLooseEnemy(ICombat enemyCombat)
    {
        attackAbleEnemies.Remove(enemyCombat);
    }
    public ICombat GetNearestEnemy()
    {
        if (attackAbleEnemies.Count == 0)
        {
            return null;
        }
        attackAbleEnemies.Sort(SortEnemy);
        attackAbleEnemies.RemoveAll((item) => item.GetGameObject() == null);
        string Enemy_array = "";
        foreach(var enemy in attackAbleEnemies)
        {
            Enemy_array = $"{Enemy_array} ,{enemy.GetTransform().name}";
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

    private int SortEnemy(ICombat lhs, ICombat rhs)
    {
        if(lhs == null)
        {
            Debug.Log(1);
            return -1;
        }
        if(rhs == null)
        {
            Debug.Log(2);
            return 1;
        }
        if(lhs.GetGameObject() == null)
        {
            Debug.Log(3);

            return -1;
        }
        if( rhs.GetGameObject() == null)
        {
            Debug.Log(4);
            return 1;
        }
        return rhs.GetDistance(transform.position).CompareTo(lhs.GetDistance(transform.position));
    }

    public void RemoveEnemy(ICombat combat)
    {
        if (attackAbleEnemies.Count == 0)
        {
            Debug.Log("Tried pop but empty");
            return;
        }
        attackAbleEnemies.Remove(combat);
    }
    public NavMeshAgent GetAgent()
    {
        return Agent_movement;
    }
    public Combat GetCombat()
    {
        return combat;
    }
    public void TakeDamage(ICombat attackerCombat, float damage)
    {
        combat.TakeDamage(attackerCombat.GetCombat(), damage);
    }
    public void Attack(ICombat targetCombat, float damage)
    {
        combat.DealDamage( targetCombat.GetCombat(),damage);
    }
    public bool IsDead()
    {
        return combat.IsDead();
    }
    public float GetDistance(Vector3 origin)
    {
        return Vector3.Distance(origin, transform.position);
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void KillNearlestEnemy()
    {
        GetNearestEnemy().GetCombat().Die();
    }

    public GameObject GetGameObject()
    {
        if (this == null)
            return null;
        return gameObject;
    }
}