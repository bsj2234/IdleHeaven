using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;

    public int CurrentWaveIndex = 0;

    public List<Wave> Waves => _waves;

    private void Start()
    {
        foreach (var wave in _waves)
        {
            wave.OnWaveCompleted += HandleOnWaveCompleted;
        }
    }

    private void HandleOnWaveCompleted()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(false);
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
}
