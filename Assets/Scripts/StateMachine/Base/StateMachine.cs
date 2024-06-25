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

    public BaseState CurrentState { get; private set; }
    protected BaseState queuedState;

    public void AddState(BaseState state)
    {
        var type = state.GetType();
        if (!states.ContainsKey(type))
        {
            states.Add(type, state);
        }
    }

    public MT ChangeState<MT>() where MT : BaseState
    {
        var type = typeof(MT);
        if (states.ContainsKey(type))
        {
            CurrentState?.ExitState(states[type]);
            CurrentState = states[type];
            CurrentState.EnterState();
            return (MT)CurrentState;
        }
        Debug.Assert(false, $"{type} is not exist on statemachine");
        return null;
    }
    public MT GetState<MT>() where MT : BaseState
    {
        var type = typeof(MT);
        if (states.ContainsKey(typeof(MT)))
        {
            return (MT)states[typeof(MT)];
        }
        Debug.Assert(false, $"{type} is not exist on statemachine");
        return null;
    }
    private void Update()
    {
        CurrentState?.UpdateState();
    }

}
