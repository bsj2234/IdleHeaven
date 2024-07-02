using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    public void LoadSceneByName(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void LoadStageScene(int stageNum)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene($"Stage{stageNum}Scene");
    }
}
