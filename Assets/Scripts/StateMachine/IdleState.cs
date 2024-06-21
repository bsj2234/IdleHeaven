using UnityEngine;

public class IdleState : BaseState
{
    private CharacterAIController _character;
    private Detector _detector;
    public IdleState(StateMachine stateMachine, CharacterAIController _controller, Detector detector) : base(stateMachine)
    {
        _character = _controller;
        _detector = detector;
        _detector.OnFoundTarget += OnFoundEnemy;
    }
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
    }
    public override void ExitState()
    {
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