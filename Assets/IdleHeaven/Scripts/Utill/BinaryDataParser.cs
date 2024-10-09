// using System.Collections.Generic;
// using UnityEngine;
// using System.Threading.Tasks;

// public interface IKeyProvider
// {
//     string Key { get; }
// }

// public class BinaryDataParser : MonoSingleton<BinaryDataParser>
// {
//     private DataManager serverDataManager;

//     private void Awake()
//     {
//         serverDataManager = GetComponent<DataManager>();
//     }

//     public async Task<Dictionary<string, T>> ParseBinaryData<T>(string endpoint) where T : IKeyProvider, new()
//     {
//         byte[] binaryData = await serverDataManager.FetchBinaryData(endpoint);
//         if (binaryData == null)
//         {
//             Debug.LogError("Failed to fetch binary data");
//             return null;
//         }

//         // Implement your binary data parsing logic here
//         // This will depend on the format of your binary data
//         Dictionary<string, T> parsedData = ParseBinaryToDict<T>(binaryData);

//         return parsedData;
//     }

//     private Dictionary<string, T> ParseBinaryToDict<T>(byte[] binaryData) where T : IKeyProvider, new()
//     {
//         // Implement your binary parsing logic here
//         // This is just a placeholder
//         Dictionary<string, T> result = new Dictionary<string, T>();
//         // ... parsing logic ...
//         return result;
//     }
// }