using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    private readonly Transform[] waypoints;
    private int currentWaypoint = 0;
    private Transform _transform;
    private readonly NavMeshAgent _navMeshAgent;

    public PatrolState(StateMachine stateMachine, Transform[] waypoints) : base (stateMachine)
    {
        this.waypoints = waypoints;
        this._transform = stateMachine.transform;
        _navMeshAgent = _transform.GetComponent<NavMeshAgent>();
    }

     public override void EnterState()
    {
        Debug.Log("Entering Patrol State");
        MoveToCurrentTarget();
    }

    public override void UpdateState()
    {
        if (waypoints.Length == 0) return;

        //_transform.position = Vector3.MoveTowards(_transform.position, waypoints[currentWaypoint].position, Time.deltaTime);

        if (Vector3.Distance(_transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            MoveToCurrentTarget();
        }
    }

    private void MoveToCurrentTarget()
    {
        Vector3 target = waypoints[currentWaypoint].position;

        _navMeshAgent.destination = target;

    }

    public override void ExitState(BaseState nextState)
    {
        Debug.Log("Exiting Patrol State");
    }
}
