using UnityEngine;
using UnityEditor;
using System.IO;
namespace IdleHeaven
{
    public static class ResourcePathCopier
    {
        [MenuItem("Assets/Copy Resource Path", false, 20)]
        private static void CopyResourcePath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path) && path.Contains("Resources"))
            {
                // Remove the "Assets/Resources/" prefix and the ".prefab" suffix
                path = path.Substring(path.IndexOf("Resources") + 10);
                path = Path.ChangeExtension(path, null);

                // Copy to clipboard
                EditorGUIUtility.systemCopyBuffer = path;
                Debug.Log("Copied Resource Path: " + path);
            }
            else
            {
                Debug.LogWarning("Selected asset is not in a Resources folder");
            }
        }

        [MenuItem("Assets/Copy Resource Path", true)]
        private static bool CopyResourcePathValidation()
        {
            // Validate the selected object
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return !string.IsNullOrEmpty(path) && path.Contains("Resources");
        }
    }
}
