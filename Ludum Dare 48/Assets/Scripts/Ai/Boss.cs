using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Ai
{
    public GameObject[] StoneProjectiles;

    public float projectileDamage = 10;
    public float projectileSpeed = 5;
    public float projectileLife = 3;
    public float deviation = 5;
    public int projectileAmount = 5;
    public Vector2 ThrowsBeforePunch;

    public GameObject SnakePrefab;
    public Vector2 SnakeAmount = new Vector2(3, 5);

    public AudioClip[] ThrowSonesSounds;
    public AudioClip PunchSound;
    public AudioClip[] StepSounds;

    private int throwsRemaining;

    public Transform ThrowPos;

    protected override void Update()
    {
        if (isDead) return;

        AiObjects.transform.position = Movement.position + (Vector3)GraphicsOffset;

        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Throw") || anim.GetCurrentAnimatorStateInfo(0).IsName("Strike");

        anim.SetBool("IsMoving", Agent.velocity.sqrMagnitude != 0);

        if (Mathf.Abs(Agent.velocity.x) > 0.01f)
        {
            anim.SetFloat("X", Agent.velocity.x);
        }

        if (!isAttacking)
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.SamplePosition(Player.instance.transform.position, out NavMeshHit hit, 15, NavMesh.AllAreas))
            {
                Agent.CalculatePath(hit.position, path);
                float pathLength = GetPathLength(path);
                if (path.status != NavMeshPathStatus.PathInvalid)
                {
                    Vector3 throwPos = Movement.position + new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), 0);

                    if (throwsRemaining <= 0)
                    {
                        Vector3 playerDir = Player.instance.transform.position - transform.position;
                        playerDir = playerDir.normalized;
                        anim.SetFloat("X", playerDir.x);
                        anim.Play("Strike");
                        Agent.isStopped = true;

                        throwsRemaining = (int)Random.Range(ThrowsBeforePunch.x, ThrowsBeforePunch.y);
                    }
                    else
                    {
                        if (!CheckWallBetween(Player.instance, throwPos) && pathLength < AttackRange)
                        {
                            Vector3 playerDir = Player.instance.transform.position - transform.position;
                            playerDir = playerDir.normalized;
                            anim.SetFloat("X", playerDir.x);
                            anim.Play("Throw");
                            throwsRemaining--;
                            Agent.isStopped = true;
                        }
                        else
                        {
                            SetDestination(Player.instance.transform.position);
                        }
                    }
                }
            }
        }
    }

    public override void AnimEvent()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
        {
            SoundManager.instance.PlaySound(ThrowSonesSounds[Random.Range(0, ThrowSonesSounds.Length)], Movement.position, 1);

            for (int i = 0; i < projectileAmount; i++)
            {
                GameObject temp = Instantiate(StoneProjectiles[Random.Range(0, StoneProjectiles.Length)]);
                Vector3 throwPos = Movement.position + new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), 0);
                temp.transform.localPosition = throwPos;

                Vector3 playerDir = Player.instance.GetPosition() - throwPos;
                playerDir = playerDir.normalized;

                float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
                angle = angle + Random.Range(-deviation, deviation);
                Quaternion bulletDev = Quaternion.Euler(new Vector3(0, 0, angle));

                temp.GetComponent<Projectile>().bulletSet(projectileDamage, projectileSpeed * Random.Range(0.9f, 1.1f), bulletDev * Vector2.right, projectileLife, true, gameObject, Mathf.Abs(ThrowPos.position.y - Movement.position.y));

                anim.SetFloat("X", playerDir.x);
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            SoundManager.instance.PlaySound(PunchSound, Movement.position, 1, 0.9f);

            StartCoroutine(SpawnSnakes());
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            SoundManager.instance.PlaySound(StepSounds[Random.Range(0, StepSounds.Length)], Movement.position, 1, 0.9f);
        }
    }

    public IEnumerator SpawnSnakes()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < Random.Range(SnakeAmount.x, SnakeAmount.y); i++)
        {
            Instantiate(SnakePrefab, Movement.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)), Quaternion.identity);
        }
    }
}
