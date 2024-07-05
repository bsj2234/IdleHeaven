using IdleHeaven;
using UnityEngine;

public class StageData : IKeyProvider
{
    private int _id;
    private string _stageName;
    private int _waveIndex;
    private string _nextStage;
    private int _targetKillCount;
    private string _mapName;
    private string _itemSpawndata;
    private string _enemySpawndata;
    private int _waveCount;

    public StageData this[int index]
    {
        get { return CSVParser.GetStageData(_stageName, index); }
    }
    public int ID { get => _id; set => _id = value; }
    public string StageName { get => _stageName; set => _stageName = value; }
    public int WaveIndex { get => _waveIndex; set => _waveIndex = value; }
    public string NextStage { get => _nextStage; set => _nextStage = value; }
    public int TargetKillCount { get => _targetKillCount; set => _targetKillCount = value; }
    public string MapName { get => _mapName; set => _mapName = value; }
    public string ItemSpawnData { get => _itemSpawndata; set => _itemSpawndata = value; }
    public string EnemySpawnData { get => _enemySpawndata; set => _enemySpawndata = value; }
    public int WaveCount { get => _waveCount; set => _waveCount = value; }

    public StageData GetNextWave()
    {
        if (WaveCount - 1 == WaveIndex)
        {
            Debug.LogError("No Next Wave");
            return null;
        }
        return CSVParser.GetStageData(StageName, WaveIndex + 1);
    }
    public StageData GetPreviousWave()
    {
        if (WaveIndex == 0)
        {
            Debug.LogError("No Previous Wave");
            return null;
        }
        return CSVParser.GetStageData(StageName, WaveIndex - 1);
    }

    public string GetKey()
    {
        return $"{StageName}-{WaveIndex}";
    }
}
