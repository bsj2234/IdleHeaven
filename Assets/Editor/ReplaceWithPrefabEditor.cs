using UnityEditor;
using UnityEngine;
/// <summary>
///  i did not know it was provied in unity
///  but still the menu was not visible sometime so i will keep it
///  
/// HOW TO USE
/// select objects and right clit ti select replace selected objects with prefab
/// </summary>
public class ReplaceWithPrefabEditor : Editor
{
    private static int objectPickerID;

    [MenuItem("GameObject/Replace Selected Objects With Prefab", false, 0)]
    private static void ReplaceWithSelectedPrefab()
    {
        if (Selection.gameObjects.Length > 0)
        {
            // Show the object picker dialog and save the control ID
            objectPickerID = EditorGUIUtility.GetControlID(FocusType.Passive);
            EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "t:Prefab", objectPickerID);
            EditorApplication.update += OnPrefabSelected;
        }
        else
        {
            Debug.LogError("No GameObjects selected for replacement.");
        }
    }

    private static void OnPrefabSelected()
    {
        // Check if the Object Picker dialog has closed
        if (EditorGUIUtility.GetObjectPickerControlID() == objectPickerID)
        {
            // Get the selected prefab
            GameObject selectedPrefab = EditorGUIUtility.GetObjectPickerObject() as GameObject;

            if (selectedPrefab != null)
            {
                foreach (GameObject objectToReplace in Selection.gameObjects)
                {
                    Replace(objectToReplace, selectedPrefab);
                }
                Debug.Log("Objects replaced successfully.");
            }
            else
            {
                Debug.LogWarning("No prefab selected.");
            }
        }
        else
        {
            EditorApplication.update -= OnPrefabSelected;
        }
    }

    [MenuItem("GameObject/Replace Selected Objects With Prefab", true)]
    private static bool ValidateReplaceWithSelectedPrefab()
    {
        return Selection.gameObjects.Length > 0;
    }

    private static void Replace(GameObject objectToReplace, GameObject prefab)
    {
        // Ensure objectToReplace is not null
        if (objectToReplace == null)
        {
            Debug.LogError("Object to replace is null.");
            return;
        }

        // Get the original position, rotation, and parent
        Vector3 originalPosition = objectToReplace.transform.position;
        Quaternion originalRotation = objectToReplace.transform.rotation;
        Transform originalParent = objectToReplace.transform.parent;

        // Instantiate the new prefab
        GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        if (newObject != null)
        {
            newObject.transform.position = originalPosition;
            newObject.transform.rotation = originalRotation;
            newObject.transform.parent = originalParent;

            // Optional: Copy necessary components or data from the original GameObject
            // Example: Copying the name
            newObject.name = objectToReplace.name;

            // Destroy the original GameObject
            DestroyImmediate(objectToReplace);
        }
        else
        {
            Debug.LogError("Failed to instantiate prefab.");
        }
    }
}
