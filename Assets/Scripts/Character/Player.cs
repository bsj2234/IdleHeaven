using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private Health _health;
    public void ResetPlayer()
    {
        StartCoroutine(DelayedReset(1f));
    }

    private IEnumerator DelayedReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        _stateMachine.ChangeState<IdleState>();
        _health.ResetDead();
    }
}
