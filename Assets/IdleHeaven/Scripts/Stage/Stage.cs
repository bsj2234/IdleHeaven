using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour
{
    [SerializeField] private Wave _wave;

    [SerializeField] private bool Looping;
    [SerializeField] private Player _player;

    public int CurrentWaveIndex = 0;


    [SerializeField] private string _stageName;

    public string StageName { get => _stageName; set => _stageName = value; }

    public UnityEvent OnStageClear;
    public UnityEvent<string, int> OnWaveChanged;
    private int _waveCount;

    private List<StageData> _waveDatas;



    public void SetStageData(StageData stageData)
    {
        _waveDatas = new List<StageData>();

        StageName = stageData.StageName;
        _waveCount = stageData.WaveCount;

        for (int i = 0; i < _waveCount; i++)
        {
            Debug.Log($"StageInit index : {i}");
            _waveDatas.Add(CSVParser.GetStageData(StageName, i));
        }
        StageData InitialStage = CSVParser.GetStageData(StageName,0);
        _wave.Init(InitialStage);


        _wave.OnWaveCompleted += HandleOnWaveCompleted;
    }

    private void HandleOnWaveCompleted()
    {
        if (Looping)
        {
            _wave.ResetWave();
        }

        CurrentWaveIndex++;
        if (CurrentWaveIndex >= _waveDatas.Count)
        {

            CurrentWaveIndex = 0;
            Debug.Log("Stage Completed");
            OnStageClear?.Invoke();
            OnWaveChanged?.Invoke(StageName ,CurrentWaveIndex);
            return;
        }
        StageData curStage = CSVParser.GetStageData(StageName, CurrentWaveIndex);
        _wave.Init(curStage);
        OnWaveChanged?.Invoke(StageName, CurrentWaveIndex);
        return;
    }


    public void OnPlayerDead(Attack attacker, Health player)
    {
        Looping = true;
        if (CurrentWaveIndex > 0)
        {
            CurrentWaveIndex--;
            _player.ResetPlayer();
            _wave.ResetWave();
            StageData curStage = CSVParser.GetStageData(StageName, CurrentWaveIndex);
            _wave.Init(curStage);
        }
        else
        {
            //스테이지 구현시 전 스테이지로 돌아가게
            _wave.ResetWave();
            _player.ResetPlayer();
        }
    }

}
