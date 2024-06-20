using System;
using UnityEngine;
using IdleHeaven;

public interface IViewModel
{
    void Refresh();
}

public interface IView
{
    void Initialize(IViewModel viewModel);
    void Refresh();
}
