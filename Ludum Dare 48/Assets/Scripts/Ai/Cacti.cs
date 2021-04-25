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

    private bool isAttacking;

    private void Update()
    {
        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        if (!isAttacking)
        {
            Vector3 throwPos = new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), ThrowPos.localPosition.y);

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
        base.Damage(damage);


    }

    public override void MoveCharacter(Vector3 by)
    {
        //Do nothing
    }

    public override void AnimEvent()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            GameObject temp = Instantiate(CactiProjectile, transform);
            Vector3 throwPos = new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), ThrowPos.localPosition.y);
            temp.transform.localPosition = throwPos;

            Vector3 playerDir = Player.instance.transform.position - (transform.position + throwPos);
            playerDir = playerDir.normalized;

            temp.transform.right = playerDir;
            temp.GetComponent<Projectile>().bulletSet(null, projectileDamage, projectileSpeed, projectileLife, true, gameObject);
        }
    }
}
