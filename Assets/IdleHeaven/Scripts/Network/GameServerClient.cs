using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using IdleHeaven;

public class GameServerClient : MonoBehaviour
{
    public static readonly string ServerUrl = "http://localhost:8080"; // Replace with your server's URL when deploying

    public static async Task<string> GetDropItem(string enemy)
    {
        string endpoint = $"/game/dropitem?enemy={enemy}";
        return await SendRequest(endpoint, UnityWebRequest.kHttpVerbGET);
    }

    public static async Task<string> SendStageClear(StageClearRequest stageClearRequest)
    {
        string endpoint = "/game/stageClear";
        string jsonBody = JsonUtility.ToJson(stageClearRequest);
        return await SendRequest(endpoint, UnityWebRequest.kHttpVerbPOST, jsonBody);
    }

    public static async Task<string> GetItemData()
    {
        string endpoint = "/game/itemdata";
        return await SendRequest(endpoint, UnityWebRequest.kHttpVerbGET);
    }

    private static async Task<string> SendRequest(string endpoint, string method, string bodyData = null)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(ServerUrl + endpoint, method))
        {
            if (!string.IsNullOrEmpty(bodyData))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyData);
                webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("auth_token", AuthManager.AuthToken);
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                return webRequest.downloadHandler.text;
            }
            else
            {
                Debug.LogError($"Request to {ServerUrl + endpoint} failed: {webRequest.error}");
                Debug.LogError($"Response Code: {webRequest.responseCode}");
                throw new Exception($"Request failed: {webRequest.error} (Status: {webRequest.responseCode})");
            }
        }
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            string result = await GetDropItem("0");
            Debug.Log("Drop Item: " + result);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StageClearRequest stageClearRequest = new StageClearRequest();
            stageClearRequest.stage = "0";
            stageClearRequest.clearTime = 100;
            stageClearRequest.playerId = "0";
            string result = await SendStageClear(stageClearRequest);
            Debug.Log("Drop Item: " + result);
        }
    }

    public class StageClearRequest
    {
        public string stage;
        public int clearTime;
        public string playerId;
    }
}