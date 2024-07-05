using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(Attack))]
[RequireComponent (typeof(Detector))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _moveDirection = Vector3.zero;
    private float _speed = 0.0f;
    private float _direction = 0.0f;
    private NavMeshAgent _agent;
    private Attack _attack;
    private Detector _detector;

    //cache
    Health health;
    float damage;
    AttackType type;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _attack = GetComponent<Attack>();
        _detector = GetComponentInChildren<Detector>();
    }

    //애니메이션 실행하고 공격 성공시 딜 넣기
    public void HandleOnAttack(Health health, float damage, AttackType type)
    {
        _animator.SetTrigger("Attack");
        this.health = health;
        this.damage = damage;
        this.type = type;
    }

    public void HandleAnimationAttack()
    {
        //attack.DealDamage(health, damage, type);
        _attack.RangeAttack(health, damage, _detector, 5f, 90f, type);
    }


    Vector3 prevDir = Vector3.zero;
    List<float> average = new List<float>();
    void FixedUpdate()
    {
        float curSpeed = _agent.velocity.magnitude;

        float DeltaDir = 0f;
        if (curSpeed > 0.1f)
        {
            Vector3 dirDiff = transform.forward - prevDir;
            dirDiff = transform.InverseTransformDirection(dirDiff);
             DeltaDir = dirDiff.x;

            _animator.SetBool("IsMoving", true);
            _animator.SetBool("IsRunning", false);
            Debug.Log("Moving");
        }
        else
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsRunning", false);
            Debug.Log("Not Moving");
        }
        if (curSpeed > 15f)
        {
            _animator.SetBool("IsMoving" +
                "", false);
            _animator.SetBool("IsRunning", true);
            Debug.Log("Running");
        }

        average.Add(DeltaDir);

        float avgDelDir = 0f;

        foreach (float dir in average)
        {
            avgDelDir += dir;
        }
        avgDelDir /= average.Count;

        if (average.Count > 5)
        {
            average.RemoveAt(0);
        }



        _animator.SetFloat("MovingDirection", avgDelDir);

        prevDir = transform.forward;
    }
}
