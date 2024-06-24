using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private List<string> _stages;

    public void LoadStage()
    {
        SceneManager.LoadScene(_stages[0]);
    }
}
