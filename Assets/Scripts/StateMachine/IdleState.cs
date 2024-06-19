using UnityEngine;

public class IdleState : BaseState
{
    private CharacterAIController _character;
    private Detector _detector;
    public IdleState(StateMachine stateMachine, CharacterAIController _controller, Detector detector) : base(stateMachine)
    {
        _character = _controller;
        _detector = detector;
        _detector.FoundTargetHandler += OnFoundEnemy;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Idle State");
    }
    public override void UpdateState()
    {
    }
    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }

    void OnFoundEnemy(Transform enemy)
    {
        if (stateMachine.CurrentState == this)
        {
            stateMachine.GetState<ChaseState>()
                .SetTarget(enemy)
                .ChangeStateTo<ChaseState>();
        }
    }
}