using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();

    public Transform Owner { get; private set; }

    public void Init(Transform owner)
    {
        this.Owner = owner;
    }

    [SerializeField] BaseState _currentState;
    public BaseState CurrentState { get => _currentState; set => _currentState = value;}

    public void AddState(BaseState state)
    {
        var type = state.GetType();
        if (!states.ContainsKey(type))
        {
            states.Add(type, state);
        }
    }

    public T ChangeState<T>() where T : BaseState
    {
        var type = typeof(T);
        if (states.ContainsKey(type))
        {
            CurrentState?.ExitState(states[type]);
            CurrentState = states[type];
            CurrentState.EnterState();
            return (T)CurrentState;
        }
        Debug.Assert(false, $"{type} is not exist on statemachine");
        return null;
    }
    public T GetState<T>() where T : BaseState
    {
        var type = typeof(T);
        if (states.ContainsKey(typeof(T)))
        {
            return (T)states[typeof(T)];
        }
        Debug.Assert(false, $"{type} is not exist on statemachine");
        return null;
    }
    private void Update()
    {
        CurrentState?.UpdateState();
    }

}
