using UnityEditor;
using UnityEngine;

public class BatchRename : EditorWindow
{
    private string baseName = "NewName";
    private int startNumber = 1;

    [MenuItem("Tools/Batch Rename")]
    public static void ShowWindow()
    {
        GetWindow<BatchRename>("Batch Rename");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Rename Selected Objects", EditorStyles.boldLabel);

        baseName = EditorGUILayout.TextField("Base Name", baseName);
        startNumber = EditorGUILayout.IntField("Start Number", startNumber);

        if (GUILayout.Button("Rename"))
        {
            RenameSelectedObjects();
        }
    }

    private void RenameSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            selectedObjects[i].name = baseName + (startNumber + i);
        }
    }
}
