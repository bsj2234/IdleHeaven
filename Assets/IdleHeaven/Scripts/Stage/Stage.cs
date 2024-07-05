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
    private int _waveCount;

    private List<StageData> _waveDatas;



    private void Start()
    {
        _wave.OnWaveCompleted += HandleOnWaveCompleted;
    }

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
            return;
        }
        StageData curStage = CSVParser.GetStageData(_stageName,CurrentWaveIndex);
        _wave.Init(curStage);
        return;
    }

    private IEnumerator DelayedSave()
    {

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SavePlayerData();
    }

    public void OnPlayerDead(Attack attacker, Health player)
    {
        Looping = true;
        if (CurrentWaveIndex > 0)
        {
            _wave.ResetWave();
            _player.ResetPlayer();
            CurrentWaveIndex--;
        }
        else
        {
            //스테이지 구현시 전 스테이지로 돌아가게
            _wave.ResetWave();
            _player.ResetPlayer();
        }
    }
}
