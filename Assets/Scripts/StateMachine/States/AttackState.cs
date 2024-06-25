using IdleHeaven;
using TMPro.EditorUtilities;
using UnityEngine;

public class AttackState : BaseState
{
    float attackCooldown = 0f;

    Transform target;
    Transform _transform;
    Health targetCombat;
    CharacterAIController _controller;
    Attack _attack;
    Detector _detector;
    CharacterStats _stats;

    bool _hasStat = false;


    public AttackState(StateMachine stateMachine, CharacterAIController controller, Attack attack, Detector detector) : base(stateMachine)
    {
        _controller = controller;
        _attack = attack;
        _detector = detector;
        _transform = stateMachine.transform;


        _hasStat = stateMachine.TryGetComponent(out CharacterStats stats);
        _stats = stats;
    }

    public AttackState SetTarget(Transform target)
    {
        this.target = target;
        targetCombat = target.GetComponent<Health>();
        Debug.Assert(targetCombat != null, $"{_controller.name} is Not Attackable Object");
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Attack State");
    }

    public override void ExitState(BaseState nextState)
    {
        Debug.Log("Exit Attack State");
    }
    public override void UpdateState()
    {

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

        Vector3 selftToTarget = target.position - _transform.position;
        bool isInAttackRange = Mathf.Abs(selftToTarget.y) < .5f && new Vector2(selftToTarget.x, selftToTarget.z).magnitude < 2f;
        if(!isInAttackRange)
        {
            stateMachine.ChangeState<ChaseState>().SetTarget(target);
            return;
        }

        bool isAttackable = attackCooldown < 0f;
        if (isAttackable)
        {
            Debug.Assert(targetCombat != null, $"Enemy is null while {_controller.transform.name} try attacking");
            DamageInfo damage = _hasStat ? _stats.GetDamage() : new DamageInfo();
            _attack.DealDamage(targetCombat, damage.Damage, damage.AttackType);
            attackCooldown = 1f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
