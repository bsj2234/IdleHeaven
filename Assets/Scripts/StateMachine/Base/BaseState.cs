using System;
using UnityEngine;

[Serializable]
public class BaseState
{
    [SerializeField] string _name;
    protected StateMachine stateMachine;

    public BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        _name = this.GetType().Name;
    }
    public virtual void EnterState()
    {
    }
    public virtual void ExitState(BaseState nextState)
    {
    }
    public virtual void UpdateState()
    {
    }
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