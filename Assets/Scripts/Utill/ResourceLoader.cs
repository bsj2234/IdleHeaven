using UnityEngine;
namespace IdleHeaven
{
    public static class ResourceLoader
    {
        /// <summary>
        /// Loads a prefab from the Resources folder using the specified path.
        /// </summary>
        /// <param name="path">The relative path to the prefab within the Resources folder (without .prefab extension).</param>
        /// <returns>The loaded prefab GameObject, or null if not found.</returns>
        public static GameObject LoadPrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogError($"Prefab not found at path: {path}");
            }
            return prefab;
        }
    }

}