using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainUi;
    [SerializeField] private Canvas _offedUi;
    public void ToggleUi()
    {
        if(_mainUi.enabled)
        {
            _mainUi.enabled = false;
            _offedUi.enabled = true;
        }
        else
        {
            _mainUi.enabled = true;
            _offedUi.enabled = false;
        }
    }
}
