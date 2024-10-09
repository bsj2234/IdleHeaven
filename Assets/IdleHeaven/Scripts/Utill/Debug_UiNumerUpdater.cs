using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Debug_UiNumerUpdater : MonoBehaviour
{
    private TMP_Text _text;
    private int _counter;

    private void Awake()
    {
        TryGetComponent(out _text);
    }

    private void Update()
    {
        //Debug_Logger.Log(_counter.ToString(), "blue", this);
        _text.text = _counter++.ToString();
    }
}
