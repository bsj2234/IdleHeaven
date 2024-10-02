using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomEditor(typeof(EditorDebugButtonMono))]
public class ButtonEventHolderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorDebugButtonMono script = (EditorDebugButtonMono)target;

        if (GUILayout.Button("Trigger Event"))
        {
            script.TriggerButtonEvent();
        }
        if (GUILayout.Button("Trigger Event 2"))
        {
            script.TriggerButtonEvent2();
        }
    }
}