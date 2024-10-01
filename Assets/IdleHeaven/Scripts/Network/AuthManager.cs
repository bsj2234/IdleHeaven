using UnityEngine;
using System.Net;
using System.Threading;
using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    private const string LoginUrl = "http://localhost:8080/login";
    private HttpListener listener;
    private string authToken;
    private Thread listenerThread;

    public void StartLogin()
    {
        Debug.Log("Starting login");
        if (string.IsNullOrEmpty(authToken))
        {
            StartLocalServer();
            Application.OpenURL(LoginUrl);
            Task.Run(HandleCallback);
        }
        else
        {
            Debug.Log("Already logged in");
        }
    }

    private void StartLocalServer()
    {
        listener = new HttpListener();
        listener.Start();
        listener.Prefixes.Add("http://localhost:1267/auth/callback/");
    }

    private async Task HandleCallback()
    {
        await ProcessOptionsCORSRequest();
        await ProcessPostRequest();
        listener.Stop();
    }

    private async Task ProcessOptionsCORSRequest()
    {
        HttpListenerContext context = null;
        try
        {
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5)); // 10-second timeout
            var getContextTask = Task.Run(() => listener.GetContext());
            var completedTask = await Task.WhenAny(getContextTask, timeoutTask);

            if (completedTask == getContextTask)
            {
                context = await getContextTask;
                HttpListenerRequest request = context.Request;
                DebugRequest(request);

                if(request.HttpMethod == "OPTIONS")
                {
                    // Handle preflight request
                    HttpListenerResponse response = context.Response;
                    response.AddHeader("Access-Control-Allow-Origin", "*");
                    response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
                    response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }
            }
            else
            {
                Debug.Log("Timeout waiting for request");
                // Handle timeout
                listener.Stop();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}");
        }
        finally
        {
            if (context != null)
            {
                context.Response.Close();
            }
        }
    }

    private async Task ProcessPostRequest()
    {
        HttpListenerContext context = null;
    try
    {
        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5)); // 10-second timeout
        var getContextTask = Task.Run(() => listener.GetContext());
        var completedTask = await Task.WhenAny(getContextTask, timeoutTask);

        context = await getContextTask;
        HttpListenerRequest request = context.Request;
        DebugRequest(request);
        // Process the request
        if (request.Url.LocalPath == "/auth/callback" && request.HttpMethod == "POST")
        {
            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                string requestBody = reader.ReadToEnd();
                Debug.Log($"Received request body (length: {requestBody.Length}): {requestBody}");
                if(!string.IsNullOrEmpty(requestBody))
                {
                    try
                    {
                        var jsonData = JsonUtility.FromJson<AuthCallbackData>(requestBody);
                        authToken = jsonData.token;
                        Debug.Log($"Parsed data - Type: {jsonData.type}, Name: {jsonData.name}, Email: {jsonData.email}, Token: {jsonData.token}");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error parsing JSON: {ex.Message}");
                    }
                }
            }

            HttpListenerResponse response = context.Response;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            string responseString = "<html><body><h1>Authentication successful</h1><p>You can close this window now.</p></body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();

            Debug.Log("Stopping listener");
        }
    }
    catch (Exception ex)
    {
        Debug.LogError($"Error: {ex.Message}");
    }
    finally
    {
        if (context != null)
        {
            context.Response.Close();
        }
    }
}

    private void DebugRequest(HttpListenerRequest request)
    {
        Debug.Log($"Request URL: {request.Url}");
        Debug.Log($"Request method: {request.HttpMethod}");
        Debug.Log($"Content type: {request.ContentType}");
        Debug.Log($"Content length: {request.ContentLength64}");
    }

    [System.Serializable]
    private class AuthCallbackData
    {
        public string type;
        public string name;
        public string email;
        public string token;
    }
}