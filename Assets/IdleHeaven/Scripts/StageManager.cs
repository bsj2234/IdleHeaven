using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawnData:IKeyProvider
{
    public string Name;
    public string[] Enemies;
    public int MaxEnemies;
    public int StageLevel;
    public float SpawnInterval;

    public string GetKey()
    {
       return Name;
    }
}

public class ItemDropTableData:IKeyProvider
{
    public string Name;
    public float Common;
    public float Uncommon;
    public float Epic;
    public float Unique;
    public float Legendary;
    public string[] WeaponDatas;
    public string[] AccessoryDatas;
    public string[] EquipmentDatas;

    public string GetKey()
    {
        return Name;
    }
}

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private Stage stage;

    [SerializeField] private Attack _playerAttack;
    [SerializeField] private ItemSpawnManager _itemSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    EnemySpawnData EnemySpawnData;


    private void Start()
    {
        StartCoroutine(DelayedLoadStage("Stage1", 1f));
        stage.OnStageClear.AddListener(LoadNextStage);
    }

    private IEnumerator DelayedLoadStage(string stageName, float time)
    {
        yield return new WaitForSeconds(time);
        LoadStage(stageName);
    }

    public void LoadNextStage()
    {
        StageData currentStage = GetCurrentStage();

        string nextStage = currentStage.NextStage;

        if (nextStage == "End")
        {
            Debug.Log("Game Clear");
            return;
        }

        LoadStage(nextStage);
    }

    public void LoadStage(string stageName)
    {
        StageData stage = CSVParser.GetStageData(stageName);
        this.stage.SetStageData(stage);
    }
    private StageData GetCurrentStage()
    {
        return CSVParser.GetStageData(stage.StageName, stage.CurrentWaveIndex);
    }
}
