using IdleHeaven;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
public class EnemyAiController : MonoBehaviour
{
    [SerializeField] bool AutoComponentSet = false;
    [SerializeField] Health _health;
    [SerializeField] Attack _attack;
    [SerializeField] Detector _detector;

    [SerializeField] UnityEngine.AI.NavMeshAgent Agent_movement;

    private StateMachine stateMachine;
    public Transform[] patrolWaypoints;
    public Transform chaseTarget;

    private void OnValidate()
    {
        if (AutoComponentSet)
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

        IdleState idleState = new IdleState(stateMachine, _detector);
        PatrolState patrolState = new PatrolState(stateMachine, patrolWaypoints);
        ChaseState chaseState = new ChaseState(stateMachine, chaseTarget, _detector);
        AttackState attackState = new AttackState(stateMachine, _attack, _detector);
        DeadState deadState = new DeadState(stateMachine);

        stateMachine.AddState(idleState);
        stateMachine.AddState(patrolState);
        stateMachine.AddState(chaseState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(deadState);

        stateMachine.ChangeState<IdleState>();
        _health.OnDead.AddListener((attack, helth) => stateMachine.ChangeState<DeadState>());

        _detector.transform.gameObject.SetActive(true);
    }
}
