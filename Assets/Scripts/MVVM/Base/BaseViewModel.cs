using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseViewModel : MonoBehaviour
{
    public event Action<string> OnPropertyChanged;
    protected void RaisePropertyChanged(string propertyName)
    {
        OnPropertyChanged?.Invoke(propertyName);
    }
}
