using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

public class GameServerClient : MonoBehaviour
{
    private const string ServerUrl = "http://localhost:8080"; // Replace with your server's URL when deploying

    public async Task<string> GetDropItem(string enemy)
    {
        return await SendRequest($"/game/dropitem?enemy={enemy}", UnityWebRequest.kHttpVerbGET);
    }

    public async Task<string> SendStageClear(StageClearRequest stageClearRequest)
    {
        string endpoint = "/game/stageClear";
        string jsonBody = JsonUtility.ToJson(stageClearRequest);
        return await SendRequest(endpoint, "POST", jsonBody);
    }

    private async Task<string> SendRequest(string endpoint, string method, string bodyData = null)
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