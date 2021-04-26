using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cacti : Character
{
    public bool isPlayerClose;
    public Animator anim;

    public GameObject CactiProjectile;

    public float projectileDamage = 10;
    public float projectileSpeed = 5;
    public float projectileLife = 3;

    public Transform ThrowPos;

    public GameObject DropWhenDead;

    public AudioClip[] AttackSounds;

    private bool isAttacking;

    private void Update()
    {
        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        if (!isAttacking)
        {
            Vector3 throwPos = new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), ThrowPos.localPosition.y);
            throwPos = transform.TransformPoint(throwPos);

            if (isPlayerClose && !CheckWallBetween(Player.instance, throwPos))
            {
                Vector3 playerDir = Player.instance.transform.position - transform.position;
                playerDir = playerDir.normalized;
                anim.SetFloat("X", playerDir.x);
                anim.Play("Attack");
            }
        }
    }

    public override void Damage(float damage)
    {
        if (isDead) return;

        base.Damage(damage);
    }

    public override void Die()
    {
        if (isDead) return;

        isDead = true;

        anim.Play("Death", 0, 0);

        foreach (var c in GetComponentsInChildren<Collider2D>())
        {
            Destroy(c);
        }

        if (DropWhenDead) Instantiate(DropWhenDead, transform.position + (Vector3.up * 0.5f), Quaternion.identity);

        Destroy(this, 0.5f);
    }
    public override void MoveCharacter(Vector3 by)
    {
        //Do nothing
    }

    public override void AnimEvent()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SoundManager.instance.PlaySound(AttackSounds[Random.Range(0, AttackSounds.Length)], transform.position, 1);

            GameObject temp = Instantiate(CactiProjectile, transform);
            Vector3 throwPos = new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), 0);
            temp.transform.localPosition = throwPos;

            throwPos.y = 0;
            throwPos = transform.TransformPoint(throwPos);


            Vector3 playerDir = ShootPrediction.FirstOrderIntercept(throwPos, Vector2.zero, projectileSpeed, Player.instance.transform.position, Player.instance.rb.velocity) - throwPos;
            playerDir = playerDir.normalized;

            //temp.transform.right = playerDir;
            temp.GetComponent<Projectile>().bulletSet(projectileDamage, projectileSpeed, playerDir, projectileLife, true, gameObject, ThrowPos.localPosition.y);

            anim.SetFloat("X", playerDir.x);
        }
    }
}
