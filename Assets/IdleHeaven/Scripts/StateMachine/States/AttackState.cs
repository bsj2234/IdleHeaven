using IdleHeaven;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    float attackCooldown = 0f;

    Transform target;
    Transform _transform;
    Health targetCombat;
    Attack _attack;
    Detector _detector;
    CharacterStats _stats;
    NavMeshAgent _agent;

    bool _hasStat = false;


    public AttackState(StateMachine stateMachine, Attack attack, Detector detector) : base(stateMachine)
    {
        _attack = attack;
        _detector = detector;
        _transform = stateMachine.transform;


        _hasStat = stateMachine.TryGetComponent(out CharacterStats stats);
        _agent = stateMachine.GetComponent<NavMeshAgent>();
        _stats = stats;
    }

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

    public override void ExitState(BaseState nextState)
    {
        attackCooldown = 0f;
        Debug.Log("Exit Attack State");
        _agent.updateRotation = true;
    }
    public override void UpdateState()
    {
        if(attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            return;
        }
        //check is daed or destroyed
        bool isDestroyed = target == null;
        if (isDestroyed)
        {
            SetTarget(_detector.GetNearestTarget());
        }
        else
        {
            bool isDead = targetCombat.IsDead();
            if (isDead)
            {
                _detector.RemoveTarget(targetCombat.transform);
                SetTarget(_detector.GetNearestTarget());
            }
        }

        if (target == null)
        {
            stateMachine.ChangeState<IdleState>();
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
            DamageInfo damage = _hasStat ? _stats.GetDamage() : new DamageInfo { AttackType = AttackType.None, Damage = 1f };
            _attack.TriggerAttack(targetCombat, damage.Damage, damage.AttackType);
            attackCooldown = .5f;
        }
    }
}
