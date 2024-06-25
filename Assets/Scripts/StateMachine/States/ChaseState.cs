using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private Transform target;
    private readonly Transform _transform;
    private readonly NavMeshAgent _navMeshAgent;
    private CharacterAIController _owner;
    private Detector _detector;

    private float destinationCoolDown = 0f;

    public ChaseState(StateMachine stateMachine, Transform target, Detector detector) : base(stateMachine)
    {
        this.target = target;
        _transform = stateMachine.transform;
        _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        _owner = stateMachine.GetComponent<CharacterAIController>();
        _detector = detector;
        _detector.OnFoundTarget += HandleFoundEnemy;
    }
    ~ChaseState() 
    {
        _detector.OnFoundTarget -= HandleFoundEnemy;
    }

    public override void EnterState()
    {
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

        if(_navMeshAgent.destination != target.position)
        {
            if(destinationCoolDown <= 0)
            {

               _navMeshAgent.destination = target.position;
                destinationCoolDown = .5f;
            }

        }
        if(destinationCoolDown > 0)
        {
            destinationCoolDown -= Time.deltaTime;
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

    public override void ExitState(BaseState nextState)
    {
    }

    public ChaseState SetTarget(Transform target)
    {
        this.target = target;
        if(stateMachine.CurrentState == this)
        {
            _navMeshAgent.destination = target.position;
        }
        return this;
    }

    void HandleFoundEnemy(Transform enemy)
    {
        if (stateMachine.CurrentState == this)
        {
            Transform target = _detector.GetNearestTarget();
            Debug.Assert(target != null);
            stateMachine.GetState<ChaseState>()
                .SetTarget(target)
                .ChangeStateTo<ChaseState>();
        }
    }
}