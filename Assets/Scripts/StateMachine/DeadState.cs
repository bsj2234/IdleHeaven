using UnityEngine;

public class DeadState : BaseState
{
    private CharacterAIController character;
    public DeadState(StateMachine stateMachine) : base(stateMachine)
    {
        base.stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Idle State");
    }
    public override void UpdateState()
    {
        // Idle behavior
    }
    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }

    ~DeadState()
    {
    }
}