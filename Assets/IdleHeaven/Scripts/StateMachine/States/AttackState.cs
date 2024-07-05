using IdleHeaven;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{

    Transform _transform;
    Health targetCombat;
    Attack _attack;
    Detector _detector;
    CharacterStats _stats;
    NavMeshAgent _agent;



    public AttackState(StateMachine stateMachine, Attack attack, Detector detector) : base(stateMachine)
    {
        _attack = attack;
        _detector = detector;
        _transform = stateMachine.transform;


        hasStat = stateMachine.TryGetComponent(out CharacterStats stats);
        _agent = stateMachine.GetComponent<NavMeshAgent>();
        _stats = stats;

        _detector.additionalConditionForCleanup.Add(IsDead);
    }

    private bool IsDead(Transform item)
    {
        if (item == null)
        {
            return true;
        }
        if(item.GetComponent<Health>().IsDead())
        {
            return true;
        }
        return false;
    }

    Transform target;
    bool hasStat = false;
    public AttackState SetTarget(Transform target)
    {
        this.target = target;
        if (target == null)
        {
            return this;
        }
        targetCombat = target.GetComponent<Health>();
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Attack State");
        _agent.updateRotation = false;
    }

    float attackCooldown = 0f;
    public override void UpdateState()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            return;
        }
        // check tager is not destroyed then dead clenup target
        bool isDestroyed = target == null;
        // if target is destroyed then find new target
        // if target is not destroyed then check target is dead
        // if target is dead then remove target from detector
        // if target is not dead then attack target
        // if target is not destroyed then attack target
        if (!isDestroyed)
        {
            if (targetCombat.IsDead())
            {
                _detector.RemoveTarget(targetCombat.transform);
                return;
            }
            SetTarget(_detector.GetNearestTarget());
        }
        else
        {
            SetTarget(_detector.GetNearestTarget());
            return;
        }

        Vector3 selftToTarget = target.position - _transform.position;

        stateMachine.transform.rotation = Quaternion.LookRotation(new Vector3(selftToTarget.x, 0f, selftToTarget.z), Vector3.up);

        bool isInAttackRange = Mathf.Abs(selftToTarget.y) < .5f && new Vector2(selftToTarget.x, selftToTarget.z).magnitude < 2f;
        if (!isInAttackRange)
        {
            stateMachine.GetState<ChaseState>()
                .SetTarget(target)
                .ChangeStateTo<ChaseState>();
            return;
        }

        bool isAttackable = attackCooldown <= 0f;
        if (isAttackable)
        {
            Debug.Assert(targetCombat != null, $"Enemy is null while {stateMachine.transform.name} try attacking");
            DamageInfo damage = hasStat ? _stats.GetDamage() : new DamageInfo { AttackType = AttackType.None, Damage = 1f };
            _attack.TriggerAttack(targetCombat, damage.Damage, damage.AttackType);
            attackCooldown = .5f;
        }
    }
    public override void ExitState(BaseState nextState)
    {
        attackCooldown = 0f;
        Debug.Log("Exit Attack State");
        _agent.updateRotation = true;
    }
}
