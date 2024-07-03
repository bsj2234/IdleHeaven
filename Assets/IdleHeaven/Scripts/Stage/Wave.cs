using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] Attack _playerAttack;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] ItemSpawner _itemSpawner;

    public event System.Action<Enemy> OnEnemyKilled;

    public event System.Action OnWaveCompleted;

    public int TargetKillCount = 100;

    public int WaveKillCount { get; private set; } = 0;

    private void OnEnable()
    {
        _playerAttack.OnKillEnemy += HandleOnKillEnemy;
    }
    private void OnDisable()
    {
        _playerAttack.OnKillEnemy -= HandleOnKillEnemy;
    }

    public void Init(ItemSpawner itemSpawner, EnemySpawner enemySpawner)
    {
        _itemSpawner.Init(itemSpawner);
        _enemySpawner.Init(enemySpawner);
    }

    private void HandleOnKillEnemy()
    {
        AddKillCount(1);
    }

    public void AddKillCount(int count)
    {
        WaveKillCount += count;

        if(WaveKillCount >= TargetKillCount)
        {
            _enemySpawner.ClearEnemies();
            OnWaveCompleted?.Invoke();
        }
    }

    public void ResetWave()
    {
        WaveKillCount = 0;
        _enemySpawner.ClearEnemies();
    }
}
