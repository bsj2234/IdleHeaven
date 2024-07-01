using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveSystem
{
    // Save Player Data to JSON
    public static void SaveData<T>(T data, string fileName)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        string path = GetFilePath(fileName);

        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"Data successfully saved to {path}");
        }
        catch (IOException ex)
        {
            Debug.LogError($"Failed to save data to {path}: {ex.Message}");
        }
    }

    // Load Player Data from JSON
    public static T LoadData<T>(string fileName)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                T data = JsonConvert.DeserializeObject<T>(json);
                Debug.Log($"Data successfully loaded from {path}");
                return data;
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to load data from {path}: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"No save file found at {path}");
        }

        return default;
    }

    // Get the file path for the save file
    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, $"{fileName}.json");
    }
}
