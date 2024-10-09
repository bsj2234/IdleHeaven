using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debug_Logger
{
    public static void Log(string message, string color = "white", MonoBehaviour obj = null)
    {
        Debug.LogFormat(LogType.Log, LogOption.None, obj, $"<color={color}>{message}</color>", obj.gameObject);
    }
}
