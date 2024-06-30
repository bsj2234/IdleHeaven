using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private float speed = 0.0f;
    private float direction = 0.0f;
    private NavMeshAgent agent;
    private Attack attack;
    private Detector detector;



    //cache
    Health health;
    float damage;
    AttackType type;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attack = GetComponent<Attack>();
        detector = GetComponentInChildren<Detector>();
    }

    //애미네션 실행하고 공격 성공시 딜 넣기
    public void HandleOnAttack(Health health, float damage, AttackType type)
    {
        anim.SetTrigger("Attack");
        this.health = health;
        this.damage = damage;
        this.type = type;
    }
    public void HandleAnimationAttack()
    {
        //attack.DealDamage(health, damage, type);
        attack.RagedAttack(health, damage, detector, 5f, 90f, type);
    }


    Vector3 prevDir = Vector3.zero;
    List<float> average = new List<float>();
    void FixedUpdate()
    {
        float curSpeed = agent.velocity.magnitude;

        float DeltaDir = 0f;
        if (curSpeed > 0.1f)
        {
            Vector3 dirDiff = transform.forward - prevDir;
            dirDiff = transform.InverseTransformDirection(dirDiff);
             DeltaDir = dirDiff.x;

            anim.SetBool("IsMoving", true);
            anim.SetBool("IsRunning", false);
            Debug.Log("Moving");
        }
        else
        {
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsRunning", false);
            Debug.Log("Not Moving");
        }
        if (curSpeed > 15f)
        {
            anim.SetBool("IsMoving" +
                "", false);
            anim.SetBool("IsRunning", true);
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



        anim.SetFloat("MovingDirection", avgDelDir);

        prevDir = transform.forward;
    }
}
