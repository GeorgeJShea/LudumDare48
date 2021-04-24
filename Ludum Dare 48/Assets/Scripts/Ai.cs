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

    public bool isPlayerClose;

    public override void Awake()
    {
        base.Awake();
        Movement = Agent.transform;
    }

    private void Update()
    {
        AiObjects.transform.position = Movement.position;

        if (isPlayerClose)
        {
            NavMeshPath path = new NavMeshPath();
            Agent.CalculatePath(Player.instance.transform.position, path);
            if (GetPathLength(path) < 6)
            {
                Agent.SetDestination(Player.instance.transform.position);
            }
        }
        else
        {
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

    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return lng;
    }
}
