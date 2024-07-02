using UnityEngine;

public class IdleState : BaseState
{
    private Detector _detector;
    public IdleState(StateMachine stateMachine, Detector detector) : base(stateMachine)
    {
        _detector = detector;
        _detector.OnFoundTarget += OnFoundEnemy;
    }
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
    }
    public override void ExitState(BaseState nextState)
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