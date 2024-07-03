using IdleHeaven;
using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private Stage stage;

    private void Start()
    {
        StartCoroutine(DelayedLoadStage("Stage11", 1f));
    }

    private IEnumerator DelayedLoadStage(string stageKey, float time)
    {
        yield return new WaitForSeconds(time);
        LoadStage(stageKey);
    }

    public void LoadNextStage()
    {
        StageData currentStage = GetCurrentStage();
        StageData nextStage = CSVParser.Instance.stages[currentStage.NextStage];
        LoadStage($"currentStage.NextStage{1}");
    }

    public void LoadStage(string key)
    {
        StageData stage = CSVParser.Instance.stages[key];
        this.stage.Init(stage);
    }
    private StageData GetCurrentStage()
    {
        return CSVParser.Instance.stages[stage.StageKey];
    }
}
