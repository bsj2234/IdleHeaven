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
    float initialAttackDelay = .5f;
    float attackDelay = .5f;


    public AttackState(StateMachine stateMachine, Attack attack, Detector detector) : base(stateMachine)
    {
        _attack = attack;
        _detector = detector;
        _transform = stateMachine.transform;


        hasStat = stateMachine.TryGetComponent(out CharacterStats stats);
        _agent = stateMachine.GetComponent<NavMeshAgent>();
        _stats = stats;

        _detector.additionalConditionForCleanup.Add(IsDead);

        _stats.GetResultStats().stats[(int)StatType.AttackSpeed].StatChanged += SetAttackSpeed;
    }

    private void SetAttackSpeed(Stat stat)
    {
        if(stat.Value <= 0f)
        {
            return;
        }
        attackDelay = initialAttackDelay / stat.Value;
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
        if (item.gameObject.activeSelf == false)
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
        _agent.updateRotation = false;
    }

    float currentAttackCooldown = 0f;
    public override void UpdateState()
    {
        if (currentAttackCooldown > 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
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
            if (targetCombat.IsDead() || targetCombat.gameObject.activeSelf == false)
            {
                _detector.RemoveTarget(targetCombat.transform);
                SetTarget(_detector.GetNearestTarget());
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

        bool isAttackable = currentAttackCooldown <= 0f;
        if (isAttackable)
        {
            Debug.Assert(targetCombat != null, $"Enemy is null while {stateMachine.transform.name} try attacking");
            DamageInfo damage = hasStat ? _stats.GetDamage() : new DamageInfo { AttackType = AttackType.None, Damage = 1f };
            _attack.TriggerAttack(targetCombat, damage.Damage, damage.AttackType);
            currentAttackCooldown = attackDelay;
        }
    }
    public override void ExitState(BaseState nextState)
    {
        currentAttackCooldown = 0f;
        _agent.updateRotation = true;
    }
}
