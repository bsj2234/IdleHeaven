using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectLoadManager : MonoSingleton<GameobjectLoadManager>
{
    private void Awake()
    {
        var uiWIndows = Resources.FindObjectsOfTypeAll<UIClosedWindow>();

        foreach (var window in uiWIndows)
        {
            if (!window.gameObject.activeSelf)
            {
                window.gameObject.SetActive(true);
            }
        }
    }
}
