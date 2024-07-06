using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] Attack _playerAttack;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] ItemSpawnManager _itemSpawner;

    public event System.Action<Enemy> OnEnemyKilled;

    public event System.Action OnWaveCompleted;

    public int TargetKillCount = 100;

    public int WaveKillCount { get; private set; } = 0;

    private void Awake()
    {
        _itemSpawner = ItemSpawnManager.Instance;
    }

    private void OnEnable()
    {
        _playerAttack.OnKillEnemy += HandleOnKillEnemy;
    }
    private void OnDisable()
    {
        _playerAttack.OnKillEnemy -= HandleOnKillEnemy;
    }

    public void Init(StageData stageData)
    {
        StageData CurrentStage = stageData;

        TargetKillCount = CurrentStage.TargetKillCount;

        ItemDropTableData itemDropData = CSVParser.Instance.ItemDropDatas[CurrentStage.ItemSpawnData];
        EnemySpawnData enemySpawnData = CSVParser.Instance.EnemySpawnDatas[CurrentStage.EnemySpawnData];

        ResetWave();
        _itemSpawner.Init(itemDropData);
        _enemySpawner.Init(enemySpawnData);
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
