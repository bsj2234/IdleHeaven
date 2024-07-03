using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private bool Looping;
    [SerializeField] private Player _player;

    public int CurrentWaveIndex = 0;

    public List<Wave> Waves => _waves;

    [SerializeField] private string _stageName;

    public string StageKey { get => _stageName; set => _stageName = value; }

    public UnityEvent OnStageClear;
    private int _waveCount;

    private void Start()
    {
        foreach (Wave wave in _waves)
        {
            wave.OnWaveCompleted += HandleOnWaveCompleted;
        }
    }

    public void Init(StageData stage)
    {
        StageKey = stage.GetKey();
        _waveCount = stage.WaveCount;

        for (int i = 0; i < _waveCount; i++)
        {
            _waves[i].Init(stage.GetItemSpawner(), stage.GetEnemySpawner());
        }
    }

    private void HandleOnWaveCompleted()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(false);

        if (Looping)
        {
            // 근데 웨이브 말고 플레이어도 다시 레벨업하고 등장하고 해야 하는데
            // 여기에는 적과 플레이어 전투(체력 마력 ) 웨이브가 리셋되어야 하지
            Waves[CurrentWaveIndex].ResetWave();
            Waves[CurrentWaveIndex].gameObject.SetActive(true);
            return;

        }

        CurrentWaveIndex++;
        if (CurrentWaveIndex >= Waves.Count)
        {
            Debug.Log("Stage Completed");
            OnStageClear?.Invoke();
            return;
        }
        Waves[CurrentWaveIndex].gameObject.SetActive(true);

    }

    private IEnumerator DelayedSave()
    {

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SavePlayerData();
    }

    private void OnEnable()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(true);
    }

    public void OnPlayerDead(Attack attacker, Health player)
    {
        Looping = true;
        if (CurrentWaveIndex > 0)
        {
            Waves[CurrentWaveIndex].ResetWave();
            _player.ResetPlayer();
            CurrentWaveIndex--;
        }
        else
        {
            //스테이지 구현시 전 스테이지로 돌아가게
            Waves[CurrentWaveIndex].ResetWave();
            _player.ResetPlayer();
        }
    }
}
