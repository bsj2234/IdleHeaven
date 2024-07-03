using IdleHeaven;
using UnityEngine;

public class StageData : IKeyProvider
{
    private string _stageName;
    private int _waveIndex;
    private string _nextStage;
    private int _targetKillCount;
    private string _mapName;
    private ItemSpawner _itemSpawner;
    private EnemySpawner _enemySpawner;
    private int _waveCount;

    public string StageName { get => _stageName; set => _stageName = value; }
    public int WaveIndex { get => _waveIndex; set => _waveIndex = value; }
    public string NextStage { get => _nextStage; set => _nextStage = value; }
    public int TargetKillCount { get => _targetKillCount; set => _targetKillCount = value; }
    public string MapName { get => _mapName; set => _mapName = value; }
    public string ItemSpawner
    {
        set
        {
            GameObject dataPrefab = Resources.Load($"StageDatas/{value}") as GameObject;
            Debug.Log($"set data in spawn{value}");
            GameObject item = Object.Instantiate(dataPrefab);
            _itemSpawner = item.GetComponent<ItemSpawner>();
        }
    }
    public ItemSpawner GetItemSpawner()
    {
        return _itemSpawner;
    }
    public string EnemySpawner
    {
        set
        {
            GameObject dataPrefab = Resources.Load($"StageDatas/{value}") as GameObject;
            GameObject item = Object.Instantiate(dataPrefab);
            item.GetComponent<EnemySpawner>();
            _enemySpawner = item.GetComponent<EnemySpawner>();
        }
    }
    public EnemySpawner GetEnemySpawner()
    {
        return _enemySpawner;
    }

    public int WaveCount { get => _waveCount; set => _waveCount = value; }

    public string GetKey()
    {
        return $"{StageName}{WaveIndex}";
    }
}
