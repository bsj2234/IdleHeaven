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
        //Transform.Destroy(stateMachine.transform.gameObject);
    }
    public override void UpdateState()
    {
        // Idle behavior
    }
    public override void ExitState(BaseState nextState)
    {
    }

    ~DeadState()
    {
    }
}