using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private bool Looping;
    [SerializeField] private Player _player;

    public int CurrentWaveIndex = 0;

    public List<Wave> Waves => _waves;

    private void Start()
    {
        foreach (Wave wave in _waves)
        {
            wave.OnWaveCompleted += HandleOnWaveCompleted;
        }
    }

    private void HandleOnWaveCompleted()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(false);

        if (Looping )
        {
            // 근데 웨이브 말고 플레이어도 다시 레벨업하고 등장하고 해야 하는데
            // 여기에는 적과 플레이어 전투(체력 마력 ) 웨이브가 리셋되어야 하지
            Waves[CurrentWaveIndex].ResetWave();
            Waves[CurrentWaveIndex].gameObject.SetActive(true);
            _player.ResetPlayer();
            return;
            
        }

        CurrentWaveIndex++;
        if(CurrentWaveIndex >= Waves.Count)
        {
            Debug.Log("Stage Completed");

            return;
        }
        Waves[CurrentWaveIndex].gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(true);
    }

    public void OnPlayerDead(Attack attacker, Health player)
    {
        Looping = true;
        if(CurrentWaveIndex > 0 )
        {
            Waves[CurrentWaveIndex].ResetWave();
            player.ResetDead();
            _player.ResetPlayer();
            CurrentWaveIndex--;
        }
        else
        {
            //스테이지 구현시 전 스테이지로 돌아가게
            Waves[CurrentWaveIndex].ResetWave();
            player.ResetDead();
            _player.ResetPlayer();
        }
    }
}
