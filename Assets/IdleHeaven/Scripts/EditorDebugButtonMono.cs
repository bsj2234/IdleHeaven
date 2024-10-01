using UnityEngine.Events;
using UnityEngine;

public class EditorDebugButtonMono : MonoBehaviour
{
    public UnityEvent buttonClickEvent;
    public AuthManager authManager;

    private void Awake() {
        #if !UNITY_EDITOR
        Debug.LogError("EditorDebugButtonMono can only be used in the editor");
        #endif
    }

    public void TriggerButtonEvent()
    {
        Debug.Log("TriggerButtonEvent");
        authManager.StartLogin();
        buttonClickEvent.Invoke();
    }
}