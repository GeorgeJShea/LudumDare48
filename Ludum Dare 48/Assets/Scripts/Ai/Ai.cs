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

    public float AttackRange = 1;
    public float AttackDamage = 5;

    public bool isPlayerClose;
    public Animator anim;
    public bool isAttacking;

    public Vector2 GraphicsOffset;

    private float playerRange = 6;

    public override void Awake()
    {
        base.Awake();
        Movement = Agent.transform;
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);

        isPlayerClose = true;
        playerRange = 30;
    }

    protected virtual void Update()
    {
        AiObjects.transform.position = Movement.position + (Vector3)GraphicsOffset;

        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        anim.SetBool("IsMoving", Agent.velocity.sqrMagnitude != 0);

        if (Mathf.Abs(Agent.velocity.x) > 0.01f)
        {
            anim.SetFloat("X", Agent.velocity.x);
        }

        if (!isAttacking)
        {
            if (isPlayerClose)
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.SamplePosition(Player.instance.transform.position, out NavMeshHit hit, 15, NavMesh.AllAreas))
                {
                    Agent.CalculatePath(hit.position, path);
                    float pathLength = GetPathLength(path);
                    if (path.status != NavMeshPathStatus.PathInvalid && pathLength < playerRange)
                    {
                        if (pathLength < 1)
                        {
                            anim.Play("Attack");
                            Agent.isStopped = true;
                        }
                        else
                        {
                            SetDestination(Player.instance.transform.position);
                        }
                    }
                    else
                    {
                        playerRange = 6;
                    }
                }

            }
            else
            {
                playerRange = 6;

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
                        SetDestination(CurrentWaypoint.position);
                    }
                }
            }
        }
    }

    public void SetDestination(Vector3 dest)
    {
        Agent.isStopped = false;
        Agent.SetDestination(dest);
    }

    public override Vector3 GetPosition()
    {
        return Movement.position;
    }

    public override void AnimEvent()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Player.instance.MovePlayer(this);
            Player.instance.Damage(AttackDamage);
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
