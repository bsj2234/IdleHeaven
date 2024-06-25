using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState
{
    protected StateMachine stateMachine;

    public BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public abstract void EnterState();
    public abstract void ExitState(BaseState nextState);
    public abstract void UpdateState();
    public virtual void ChangeStateTo<T>() where T : BaseState
    {
        Type type = this.GetType();
        stateMachine.ChangeState<T>();
    }
}

public interface ITriggerBaseState
{
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
    public abstract void OnTriggerExit(Collider other);
}