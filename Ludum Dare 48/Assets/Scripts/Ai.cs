using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : Character
{
    public Transform AiObjects;
    public NavMeshAgent Agent;
    [HideInInspector] public Transform Movement;
    public Area PatrolArea;

    public Vector2 WaitTimer = new Vector2(1, 2);
    private float waitTimer = 1;
    [HideInInspector] public Transform CurrentWaypoint;

    public override void Awake()
    {
        base.Awake();
        Movement = Agent.transform;
    }

    private void Update()
    {
        AiObjects.transform.position = Movement.position;

        if (PatrolArea == null) return;

        if (!CurrentWaypoint)
        {
            waitTimer = Random.Range(WaitTimer.x, WaitTimer.y);
            CurrentWaypoint = PatrolArea.GetWaypoint();
        }

        if (CurrentWaypoint)
        {
            if (Vector2.Distance(Movement.position, CurrentWaypoint.position) < 0.05f)
            {
                if (waitTimer <= 0)
                {
                    CurrentWaypoint = null;
                }
                else
                {
                    waitTimer -= Time.deltaTime;
                }
            }
            else
            {
                Agent.SetDestination(CurrentWaypoint.position);
            }
        }
    }
}
