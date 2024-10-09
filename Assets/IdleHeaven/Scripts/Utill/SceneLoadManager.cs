using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    public string[] _sceneNames;
    private List<AsyncOperation> _opList = new List<AsyncOperation>();
    private List<Scene> _sceneList = new List<Scene>();

    public string _initialSceneName;

    private void Awake()
    {
        //InitAdditiveScenes();
        LoadScene(_initialSceneName);
        Debug_Logger.Log("Scene Load Manager Awake", "green", this);
    }

    public void LoadScene(string sceneName)
    {
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        op.allowSceneActivation = false;

        op.completed += (operation) =>
        {
            op.allowSceneActivation = true;
        };
    }






    public void InitAdditiveScenes()
    {
        var sceneCount = _sceneNames.Length;
        var loadedSceneCount = 0;
        foreach (var sceneName in _sceneNames)
        {
            var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            op.allowSceneActivation = false;
            _opList.Add(op);
            _sceneList.Add(SceneManager.GetSceneByName(sceneName));

            op.completed += (operation) =>
            {
                loadedSceneCount++;
                if (loadedSceneCount == sceneCount)
                {
                    Debug_Logger.Log("Scene Load Complete", "green", this);
                    foreach (var op in _opList)
                    {
                        op.allowSceneActivation = true;
                    }
                    //StartCoroutine(asyncDeactivate());
                }
            };
        }
        StartCoroutine(LoadSceneCoroutine(_opList));
    }

    private IEnumerator asyncDeactivate()
    {
        foreach (var scene in _sceneList)
        {
            foreach (var obj in scene.GetRootGameObjects())
            {
                obj.SetActive(false);
                yield return null;
            }
        }
        Debug_Logger.Log("Scene Deactivate Complete", "green", this);
    }
    private IEnumerator LoadSceneCoroutine(List<AsyncOperation> sceneList)
    {
        foreach (var scene in sceneList)
        {
            yield return scene;
        }
    }
}
