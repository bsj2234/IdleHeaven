using UnityEngine;
using System.Net;
using System.Threading;
using System;
using System.IO;
using System.Collections;
using System.Threading.Tasks;
using System.Reflection;
public class AuthManager : MonoBehaviour
{
    private const string LoginUrl = "http://localhost:8080/login";
    private HttpListener listener;
    private string authToken;

    public void StartLogin()
    {
        if (listener != null && listener.IsListening)
        {
            listener.Close();
        }
        ForceCloseAllListeners();

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
            authToken = null;
        }
    }

    private void StartLocalServer()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:1267/auth/callback/");
        listener.Start();
    }

    private async Task HandleCallback()
    {
        await ProcessRequest();
        listener.Stop();
    }

    private async Task ProcessRequest()
    {
        HttpListenerContext context = null;
        while (listener != null && listener.IsListening)
        {
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5)); // 10-second timeout
            var getContextTask = Task.Run(() => listener.GetContext());
            var completedTask = await Task.WhenAny(getContextTask, timeoutTask);

            if (completedTask == getContextTask)
            {
                context = await getContextTask;
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                DebugRequest(request);

                // Add CORS headers for all requests
                response.AddHeader("Access-Control-Allow-Origin", "http://localhost:8080");
                response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");
                response.AddHeader("Access-Control-Allow-Credentials", "true");

                if (request.HttpMethod == "OPTIONS")
                {
                    // Handle preflight request
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }
                else if (request.HttpMethod == "POST")
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string requestBody = reader.ReadToEnd();
                        Debug.Log($"Received request body (length: {requestBody.Length}): {requestBody}");
                        if (!string.IsNullOrEmpty(requestBody))
                        {
                            try
                            {
                                var jsonData = JsonUtility.FromJson<AuthCallbackData>(requestBody);
                                authToken = jsonData.token;
                                Debug.Log($"Parsed data - Type: {jsonData.type}, Name: {jsonData.name}, Email: {jsonData.email}, Token: {jsonData.token}");
                                listener.Close();
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError($"Error parsing JSON: {ex.Message}");
                                listener.Close();
                            }
                        }
                    }

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.ContentType = "text/plain";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Success");
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    response.Close();
                    Debug.Log("Stopping listener");
                }
            }

            else
            {
                Debug.Log("Timeout waiting for request");
                // Handle timeout
                listener.Stop();
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

    public static void ForceCloseAllListeners()
    {
        var httpListenerType = typeof(HttpListener);
        var isListeningProperty = httpListenerType.GetProperty("IsListening", BindingFlags.NonPublic | BindingFlags.Static);
        var currentInstancesField = httpListenerType.GetField("_currentInstances", BindingFlags.NonPublic | BindingFlags.Static);

        if (isListeningProperty != null && currentInstancesField != null)
        {
            var currentInstances = (Hashtable)currentInstancesField.GetValue(null);
            foreach (HttpListener listener in currentInstances.Values)
            {
                try
                {
                    listener.Close();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error closing listener: {ex.Message}");
                }
            }
        }
    }
}

// using UnityEngine;
// using System.Net;
// using System;
// using System.IO;
// using System.Reflection;
// using System.Collections;
// using Cysharp.Threading.Tasks;

// public class AuthManager : MonoBehaviour
// {
//     private const string LoginUrl = "http://localhost:8080/login";
//     private HttpListener listener;
//     private string authToken;
//     private const int TimeoutSeconds = 10;

//     public static void ForceCloseAllListeners()
//     {
//         var httpListenerType = typeof(HttpListener);
//         var isListeningProperty = httpListenerType.GetProperty("IsListening", BindingFlags.NonPublic | BindingFlags.Static);
//         var currentInstancesField = httpListenerType.GetField("_currentInstances", BindingFlags.NonPublic | BindingFlags.Static);

//         if (isListeningProperty != null && currentInstancesField != null)
//         {
//             var currentInstances = (Hashtable)currentInstancesField.GetValue(null);
//             foreach (HttpListener listener in currentInstances.Values)
//             {
//                 try
//                 {
//                     listener.Close();
//                 }
//                 catch (Exception ex)
//                 {
//                     Debug.LogError($"Error closing listener: {ex.Message}");
//                 }
//             }
//         }
//     }

//     public void StartLogin()
//     {
//         DebugLog("Starting login");
//         if (string.IsNullOrEmpty(authToken))
//         {
//             Application.OpenURL(LoginUrl);
//             StartLocalServer();
//         }
//         else
//         {
//             DebugLog("Already logged in");
//             authToken = null;
//         }
//     }

//     private void CloseListener()
//     {
//         if (listener != null)
//         {
//             listener.Close();
//         }
//     }
//     private void StartLocalServer()
//     {
//         if (listener != null)
//         {
//             CloseListener();
//         }

//         listener = new HttpListener();
//         listener.Prefixes.Add("http://localhost:1267/auth/callback/");
//         SetListenerTimeout(listener, TimeSpan.FromSeconds(TimeoutSeconds));

//         listener.Start();
//         HandleCallback().Forget();
//     }

//     private void SetListenerTimeout(HttpListener listener, TimeSpan timeout)
//     {
//         var timeoutManagerProperty = typeof(HttpListener).GetProperty("TimeoutManager", BindingFlags.Instance | BindingFlags.NonPublic);
//         var timeoutManager = timeoutManagerProperty?.GetValue(listener);

//         if (timeoutManagerProperty != null)
//         {
//             var drainEntityProperty = timeoutManager.GetType().GetMethod("SetTimeout", BindingFlags.Instance | BindingFlags.NonPublic);
//             drainEntityProperty?.Invoke(timeoutManager, new object[] { (int)timeout.TotalMilliseconds });
//         }
//     }

//     private async UniTaskVoid HandleCallback()
//     {
//         while (true)
//         {
//             if (listener == null || !listener.IsListening)
//             {
//                 DebugLog("Listener is null or not listening");
//                 break;
//             }
//             try
//             {
//                 if (listener == null)
//                 {
//                     DebugLog("Listener is null");
//                     break;
//                 }
//                 var context = await listener.GetContextAsync().AsUniTask();
//                 HttpListenerRequest request = context.Request;
//                 HttpListenerResponse response = context.Response;
//                 DebugRequest(request);

//                 if (request.HttpMethod == "OPTIONS")
//                 {
//                     HandleOptionsRequest(response);
//                 }
//                 else if (request.HttpMethod == "POST")
//                 {
//                     await HandlePostRequest(request, response);
//                 }
//             }
//             catch (HttpListenerException ex)
//             {
//                 HandleListenerException(ex);
//             }
//         }
//     }

//     private void HandleOptionsRequest(HttpListenerResponse response)
//     {
//         response.AddHeader("Access-Control-Allow-Origin", "*");
//         response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
//         response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");
//         response.AddHeader("Access-Control-Max-Age", "86400");
//         response.StatusCode = (int)HttpStatusCode.OK;
//         response.Close();
//     }

//     private async UniTask HandlePostRequest(HttpListenerRequest request, HttpListenerResponse response)
//     {
//         // Add CORS headers to the POST response
//         response.AddHeader("Access-Control-Allow-Origin", "*");
//         response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
//         response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");

//         string requestBody = await ReadRequestBody(request);
//         DebugLog($"Received request body (length: {requestBody.Length}):");
//         DebugLog(requestBody);  // Log the full response

//         if (!string.IsNullOrEmpty(requestBody))
//         {
//             if (requestBody.TrimStart().StartsWith("{"))
//             {
//                 // Handle JSON response
//                 try
//                 {
//                     var jsonData = JsonUtility.FromJson<AuthCallbackData>(requestBody);
//                     authToken = System.Web.HttpUtility.UrlEncode(jsonData.token);
//                     DebugLog($"Parsed JSON data - Type: {jsonData.type}, Name: {jsonData.name}, Email: {jsonData.email}, Token: {authToken}");
//                 }
//                 catch (Exception jsonEx)
//                 {
//                     DebugError($"Error parsing JSON: {jsonEx.Message}");
//                 }
//             }
//             else if (requestBody.TrimStart().StartsWith("<"))
//             {
//                 // Handle HTML response
//                 var tokenMatch = System.Text.RegularExpressions.Regex.Match(requestBody, @"const\s+token\s*=\s*['""]([^'""]+)['""]");
//                 if (tokenMatch.Success)
//                 {
//                     authToken = System.Web.HttpUtility.UrlEncode(tokenMatch.Groups[1].Value.Trim());
//                     DebugLog($"Extracted token from HTML: {authToken}");
//                 }
//                 else
//                 {
//                     DebugError("Failed to extract token from HTML");
//                     DebugLog("HTML content:");
//                     DebugLog(requestBody);  // Log the full HTML for debugging
//                 }
//             }
//             else
//             {
//                 DebugError($"Received unknown response format: {requestBody.Substring(0, Math.Min(50, requestBody.Length))}...");
//             }
//         }

//         SendResponse(response);
//     }

//     private async UniTask<string> ReadRequestBody(HttpListenerRequest request)
//     {
//         using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
//         {
//             return await reader.ReadToEndAsync();
//         }
//     }

//     private void SendResponse(HttpListenerResponse response)
//     {
//         // Add CORS headers to the final response
//         response.AddHeader("Access-Control-Allow-Origin", "*");
//         response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
//         response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");

//         string responseString = @"
//             <html>
//             <body>
//                 <h1>Authentication successful</h1>
//                 <p>This window will close automatically.</p>
//                 <script>
//                     setTimeout(function() {
//                         window.close();
//                     }, 2000); // Close the window after 2 seconds
//                 </script>
//             </body>
//             </html>";

//         byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
//         response.ContentLength64 = buffer.Length;
//         response.ContentType = "text/html";
//         response.OutputStream.Write(buffer, 0, buffer.Length);
//         response.Close();
//     }

//     private void HandleListenerException(HttpListenerException ex)
//     {
//         if (ex.ErrorCode == 995)
//         {
//             Debug.Log("GetContext timed out. Continuing to listen...");
//         }
//         else
//         {
//             Debug.LogError($"Error in HandleCallback: {ex}");
//         }
//     }

//     private void DebugRequest(HttpListenerRequest request)
//     {
//         DebugLog($"Request URL: {request.Url}");
//         DebugLog($"Request method: {request.HttpMethod}");
//         DebugLog($"Content type: {request.ContentType}");
//         DebugLog($"Content length: {request.ContentLength64}");
//     }

//     [System.Serializable]
//     private class AuthCallbackData
//     {
//         public string type;
//         public string name;
//         public string email;
//         public string token;
//     }

//     private void DebugError(string message)
//     {
//         Debug.LogError($"[AuthManager] Error: {message}");
//     }
//     private void DebugLog(string message)
//     {
//         Debug.Log($"[AuthManager] {message}");
//     }
// }










// using UnityEngine;
// using System.Net;
// using System.Threading;
// using System;
// using System.IO;
// using System.Collections;
// using System.Threading.Tasks;
// using System.Reflection;
// public class AuthManager : MonoBehaviour
// {
//     private const string LoginUrl = "http://localhost:8080/login";
//     private HttpListener listener;
//     private string authToken;

//     public void StartLogin()
//     {
//         if (listener != null && listener.IsListening)
//         {
//             listener.Close();
//         }
//         ForceCloseAllListeners();

//         Debug.Log("Starting login");
//         if (string.IsNullOrEmpty(authToken))
//         {
//             StartLocalServer();
//             Application.OpenURL(LoginUrl);
//             Task.Run(HandleCallback);
//         }
//         else
//         {
//             Debug.Log("Already logged in");
//             authToken = null;
//         }
//     }

//     private void StartLocalServer()
//     {
//         listener = new HttpListener();
//         listener.Start();
//         listener.Prefixes.Add("http://localhost:1267/auth/callback/");
//     }

//     private async Task HandleCallback()
//     {
//         await ProcessRequest();
//         listener.Stop();
//     }

//     private async Task ProcessRequest()
//     {
//         HttpListenerContext context = null;
//         while (listener != null && listener.IsListening)
//         {
//             var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5)); // 10-second timeout
//             var getContextTask = Task.Run(() => listener.GetContext());
//             var completedTask = await Task.WhenAny(getContextTask, timeoutTask);

//             if (completedTask == getContextTask)
//             {
//                 context = await getContextTask;
//                 HttpListenerRequest request = context.Request;
//                 DebugRequest(request);

//                 if (request.HttpMethod == "OPTIONS")
//                 {
//                     // Handle preflight request
//                     HttpListenerResponse response = context.Response;
//                     response.AddHeader("Access-Control-Allow-Origin", "*");
//                     response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
//                     response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
//                     response.StatusCode = (int)HttpStatusCode.OK;
//                     response.Close();
//                 }
//                 else if (request.HttpMethod == "POST")
//                 {
//                     using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
//                     {
//                         string requestBody = reader.ReadToEnd();
//                         Debug.Log($"Received request body (length: {requestBody.Length}): {requestBody}");
//                         if (!string.IsNullOrEmpty(requestBody))
//                         {
//                             try
//                             {
//                                 var jsonData = JsonUtility.FromJson<AuthCallbackData>(requestBody);
//                                 authToken = jsonData.token;
//                                 Debug.Log($"Parsed data - Type: {jsonData.type}, Name: {jsonData.name}, Email: {jsonData.email}, Token: {jsonData.token}");
//                                 listener.Close();
//                             }
//                             catch (Exception ex)
//                             {
//                                 Debug.LogError($"Error parsing JSON: {ex.Message}");
//                                 listener.Close();
//                             }
//                         }
//                     }

//                     HttpListenerResponse response = context.Response;        // Add CORS headers to the final response
//                     response.AddHeader("Access-Control-Allow-Origin", "*");
//                     response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
//                     response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");

//                     response.Close();
//                     Debug.Log("Stopping listener");
//                 }
//             }

//             else
//             {
//                 Debug.Log("Timeout waiting for request");
//                 // Handle timeout
//                 listener.Stop();
//             }
//         }

//     }

//     private void DebugRequest(HttpListenerRequest request)
//     {
//         Debug.Log($"Request URL: {request.Url}");
//         Debug.Log($"Request method: {request.HttpMethod}");
//         Debug.Log($"Content type: {request.ContentType}");
//         Debug.Log($"Content length: {request.ContentLength64}");
//     }

//     [System.Serializable]
//     private class AuthCallbackData
//     {
//         public string type;
//         public string name;
//         public string email;
//         public string token;
//     }

//     public static void ForceCloseAllListeners()
//     {
//         var httpListenerType = typeof(HttpListener);
//         var isListeningProperty = httpListenerType.GetProperty("IsListening", BindingFlags.NonPublic | BindingFlags.Static);
//         var currentInstancesField = httpListenerType.GetField("_currentInstances", BindingFlags.NonPublic | BindingFlags.Static);

//         if (isListeningProperty != null && currentInstancesField != null)
//         {
//             var currentInstances = (Hashtable)currentInstancesField.GetValue(null);
//             foreach (HttpListener listener in currentInstances.Values)
//             {
//                 try
//                 {
//                     listener.Close();
//                 }
//                 catch (Exception ex)
//                 {
//                     Debug.LogError($"Error closing listener: {ex.Message}");
//                 }
//             }
//         }
//     }
// }