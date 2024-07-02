using UnityEngine;
using UnityEditor;
using System.IO;

public class RenameAssetsEditor : EditorWindow
{
    string baseName = "NewAssetName";
    int startNumber = 1;

    [MenuItem("Tools/Rename Selected Assets with Numbers")]
    public static void ShowWindow()
    {
        GetWindow<RenameAssetsEditor>("Rename Assets with Numbers");
    }

    void OnGUI()
    {
        GUILayout.Label("Rename Selected Assets with Numbers", EditorStyles.boldLabel);
        baseName = EditorGUILayout.TextField("Base Name", baseName);
        startNumber = EditorGUILayout.IntField("Start Number", startNumber);

        if (GUILayout.Button("Rename"))
        {
            RenameSelectedAssets();
        }
    }

    void RenameSelectedAssets()
    {
        Object[] selectedObjects = Selection.objects;

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Object obj = selectedObjects[i];
            string path = AssetDatabase.GetAssetPath(obj);
            string extension = Path.GetExtension(path);
            string directory = Path.GetDirectoryName(path);
            string newName = $"{baseName}{startNumber + i}{extension}";
            string newPath = Path.Combine(directory, newName);

            AssetDatabase.RenameAsset(path, Path.GetFileNameWithoutExtension(newName));
            AssetDatabase.SaveAssets();

            Debug.Log($"Renamed asset at {path} to {newPath}");
        }
    }
}
