using IdleHeaven;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Attack))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private Attack _attack;
    private Detector _detector;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _attack = GetComponent<Attack>();
        _detector = GetComponentInChildren<Detector>();
        Assert.IsNotNull( _detector );
    }



    //cache
    Health cachedHealth;
    float cachedDamage;
    AttackType cachedAttackType;
    //애니메이션 실행하고 공격 성공시 딜 넣기
    public void HandleOnAttack(Health health, float damage, AttackType type)
    {
        _animator.SetTrigger("Attack");
        this.cachedHealth = health;
        this.cachedDamage = damage;
        this.cachedAttackType = type;
    }
    public void HandleAnimationAttack()
    {
        //attack.DealDamage(health, damage, type);
        _attack.RangeAttack(cachedHealth, cachedDamage, _detector, 5f, 90f, cachedAttackType);
    }


    //부드러운 회전 관련
    Vector3 prevDir = Vector3.zero;
    CircularBuffer circularBuffer = new CircularBuffer(5);
    void FixedUpdate()
    {
        if (IsRunning())
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsRunning", true);
            Debug.Log("Running");
        }
        if (IsMoving())
        {
            _animator.SetBool("IsMoving", true);
            _animator.SetBool("IsRunning", false);
            Debug.Log("Moving");
        }
        if (!IsMoving())
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsRunning", false);
            Debug.Log("Not Moving");
        }



        //부드러운 회전
        float DeltaDirX = 0f;

        Vector3 worldDirDiffVec = transform.forward - prevDir;
        Vector3 localDirDiffVec = transform.InverseTransformDirection(worldDirDiffVec);
        DeltaDirX = localDirDiffVec.x;

        circularBuffer.Enqueue(DeltaDirX);
        float avgDelDir = circularBuffer.GetAverage();
        _animator.SetFloat("MovingDirection", avgDelDir);

        prevDir = transform.forward;
    }


    [SerializeField] private float _moveSpeed = 0.1f;
    [SerializeField] private float _runSpeed = 0.5f;
    private bool IsMoving()
    {
        return _agent.velocity.magnitude > _moveSpeed;
    }
    private bool IsRunning()
    {
        return _agent.velocity.magnitude > _runSpeed;
    }

}

