using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_FrameDebugLogger : MonoBehaviour
{
    private float _prevTime;
    private int _frameCount;

    private void Update()
    {
        _frameCount++;
        if (Time.time - _prevTime >= 1f)
        {
            Debug_Logger.Log($"FrameTime: {Time.time - _prevTime}", "blue", this);
            _prevTime = Time.time;
        }
    }
}
