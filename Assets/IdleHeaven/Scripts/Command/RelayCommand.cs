using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayCommand : ICommand
{
    private readonly Action execute;
    private readonly Func<bool> canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public void Execute()
    {
        if (CanExecute())
        {
            execute();
        }
    }

    public bool CanExecute()
    {
        return canExecute == null || canExecute();
    }
}