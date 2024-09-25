using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public static class UnityWebRequestExtension
{
    public static TaskAwaiter<UnityWebRequest> GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<UnityWebRequest>();
        asyncOp.completed += obj => { tcs.SetResult(((UnityWebRequestAsyncOperation)obj).webRequest); };
        return tcs.Task.GetAwaiter();
    }
}