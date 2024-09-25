using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class GameServerClient : MonoBehaviour
{
    private string playerId;
    private const string ServerUrl = "http://localhost:8080"; // Replace with your server's URL when deploying

    public async Task<string> GetDropItem()
    {
        return await SendRequest($"/game/dropitem?playerId={playerId}", UnityWebRequest.kHttpVerbPOST);
    }

    private async Task<string> SendRequest(string endpoint, string method)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(ServerUrl + endpoint, method))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // Send the request and wait for a response
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            else
            {
                return webRequest.downloadHandler.text;
            }
        }
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            string result = await GetDropItem();
            Debug.Log("Game State: " + result);
        }
    }
}