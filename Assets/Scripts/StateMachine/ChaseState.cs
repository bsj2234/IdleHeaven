using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private Transform target;
    private readonly Transform _transform;
    private readonly NavMeshAgent _navMeshAgent;
    private CharacterAIController _owner;
    private Detector _detector;

    public ChaseState(StateMachine stateMachine, Transform target, Detector detector) : base(stateMachine)
    {
        this.target = target;
        _transform = stateMachine.transform;
        _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        _owner = stateMachine.GetComponent<CharacterAIController>();
        _detector = detector;
        _detector.FoundTargetHandler += OnFoundEnemy;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Chase State");
        if (target == null) return;


        //_transform.position = Vector3.MoveTowards(_transform.position, target.position, Time.deltaTime);
        _navMeshAgent.destination = target.position;
    }

    public override void UpdateState()
    {
        if (target == null)
        {
            Transform NearestEnemy = _detector.GetNearestTarget();
            if (NearestEnemy == null)
            {
                stateMachine.ChangeState<IdleState>();
                return;
            }
            SetTarget(NearestEnemy.transform);
            if (target == null)
            {
                stateMachine.ChangeState<IdleState>();
                return;
            }
        }
        Vector3 selftToTarget = target.position - _transform.position;

        bool isInAttackRange = Mathf.Abs(selftToTarget.y) < .5f && new Vector2(selftToTarget.x, selftToTarget.z).magnitude < 2f;
        if (isInAttackRange)
        {
            stateMachine
                .GetState<AttackState>()
                .SetTarget(target)
                .ChangeStateTo<AttackState>();
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Chase State");
    }

    public ChaseState SetTarget(Transform target)
    {
        this.target = target;
        if(stateMachine.CurrentState == this)
        {
            _navMeshAgent.destination = target.position;
            Debug.Log("Update target");
        }
        return this;
    }

    void OnFoundEnemy(Transform enemy)
    {
        if (stateMachine.CurrentState == this)
        {
            stateMachine.GetState<ChaseState>()
                .SetTarget(enemy)
                .ChangeStateTo<ChaseState>();
        }
    }
}