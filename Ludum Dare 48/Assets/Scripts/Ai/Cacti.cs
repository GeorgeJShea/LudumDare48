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
            if (isPlayerClose)
            {
                Vector3 playerDir = Player.instance.transform.position - transform.position;
                playerDir = playerDir.normalized;
                anim.SetFloat("X", playerDir.x);
                anim.Play("Attack");
            }
        }
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
            temp.transform.localPosition = new Vector3(ThrowPos.localPosition.x * Mathf.Sign(anim.GetFloat("X")), ThrowPos.localPosition.y);

            Vector3 playerDir = Player.instance.transform.position - transform.position;
            playerDir = playerDir.normalized;

            temp.transform.right = playerDir;
            temp.GetComponent<Projectile>().bulletSet(null, projectileDamage, projectileSpeed, projectileLife, true, gameObject);
        }
    }
}
