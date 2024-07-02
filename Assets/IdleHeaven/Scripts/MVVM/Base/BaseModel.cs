using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel
{
    public event Action<string> PropertyChanged;

    protected void NotifyPropertyChanged(string propertyName)
    {
        if(PropertyChanged == null)
        {
            Debug.LogWarning($"Trying Notify null Action at Invoke in : {this.GetType().Name} ");
        }
        PropertyChanged?.Invoke(propertyName);
    }
}
