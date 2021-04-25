using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skelly : Ai
{
    public GameObject BoneProjectile;

    public float projectileDamage = 10;
    public float projectileSpeed = 5;
    public float projectileLife = 3;

    public Transform ThrowPos;

    protected override void Update()
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
                        Vector3 throwPos = new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), ThrowPos.localPosition.y);
                        throwPos = transform.TransformPoint(throwPos);

                        if (isPlayerClose && !CheckWallBetween(Player.instance, throwPos) && pathLength < AttackRange)
                        {
                            Vector3 playerDir = Player.instance.transform.position - transform.position;
                            playerDir = playerDir.normalized;
                            anim.SetFloat("X", playerDir.x);
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
                        playerRange = 10;
                    }
                }

            }
            else
            {
                playerRange = 10;

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

    public override void AnimEvent()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            GameObject temp = Instantiate(BoneProjectile);
            Vector3 throwPos = Movement.position + new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), 0);
            temp.transform.localPosition = throwPos;

            //throwPos.y = 0;


            Vector3 playerDir = ShootPrediction.FirstOrderIntercept(throwPos, Vector2.zero, projectileSpeed, Player.instance.transform.position, Player.instance.rb.velocity) - throwPos;
            playerDir = playerDir.normalized;

            //temp.transform.right = playerDir;
            temp.GetComponent<Projectile>().bulletSet(null, projectileDamage, projectileSpeed, playerDir, projectileLife, true, gameObject, Mathf.Abs(ThrowPos.position.y - Movement.position.y));

            anim.SetFloat("X", playerDir.x);
        }
    }
}
