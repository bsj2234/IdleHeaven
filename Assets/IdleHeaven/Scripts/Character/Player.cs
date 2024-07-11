using IdleHeaven;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private Health _health;
    [SerializeField] private CharacterStats _playerStats;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private PlayerData _playerData;

    private void Awake()
    {
        _playerStats.GetResultStats().stats[(int)StatType.Speed].StatChanged += SetMoveSpeed;
    }

    private void SetMoveSpeed(Stat stat)
    {
        _navMeshAgent.speed = stat.Value;
    }

    public void ResetPlayer()
    {
        _health.ResetDead();
        StartCoroutine(DelayedReset(1f));
    }

    private IEnumerator DelayedReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        _stateMachine.ChangeState<IdleState>();
    }

}
