using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private Health _health;
    [SerializeField] private CharacterStats _playerStats;
    [SerializeField] private Inventory _inventory;

    private PlayerData _playerData;

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

    public void TestLoad()
    {
        _playerData = DataManager.Instance.TestLoad();

        _playerStats.LevelSystem.Level = _playerData.Level;
        _playerStats.LevelSystem.Exp = _playerData.Experience;

        _inventory.Items = _playerData.Inventory.Items;

    }
}
