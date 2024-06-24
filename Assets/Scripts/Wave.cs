using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] Attack _playerAttack;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] private List<Enemy> _enemies;

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
}
