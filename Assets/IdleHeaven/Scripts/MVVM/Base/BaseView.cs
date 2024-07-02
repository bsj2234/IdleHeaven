using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView<T> : MonoBehaviour where T : BaseViewModel
{
    protected T ViewModel { get; private set; }

    protected virtual void Awake()
    {
        ViewModel = GetComponent<T>();
        ViewModel.OnPropertyChanged += OnViewModelPropertyChanged;
    }

    protected virtual void Destroyed()
    {
        ViewModel.OnPropertyChanged -= OnViewModelPropertyChanged;
    }

    protected abstract void OnViewModelPropertyChanged(string propertyName);
}
