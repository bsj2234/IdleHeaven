using System;
using System.IO;
using UnityEngine;

namespace IdleHeaven
{
    public class JsonSaveLoad
    {
        public static void SaveToJson<T>(T data, string fileName)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                File.WriteAllText(path, json);
                Debug.Log($"Data saved to {path}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data to {path}. Exception: {ex}");
            }
        }

        public static T LoadFromJson<T>(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonUtility.FromJson<T>(json);
                }
                else
                {
                    Debug.LogWarning($"No file found at {path}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load data from {path}. Exception: {ex}");
            }

            return default;
        }
    }
}