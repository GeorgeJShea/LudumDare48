using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHitable
{
    public float StartHealth = 100;
    public bool isDead;
    public Renderer rend;

    public AudioClip[] HitSounds = new AudioClip[0];

    public float Health { get { return health; } set { health = value; } }
    protected float health;

    public virtual void Awake()
    {
        health = StartHealth;
    }

    public virtual void Damage(float damage, bool makeHitSound = true)
    {
        StartCoroutine(Flicker());

        if (makeHitSound)
        {
            if (HitSounds.Length > 0)
            {
                SoundManager.instance.PlaySound(HitSounds[Random.Range(0, HitSounds.Length)], GetPosition(), 1);
            }
        }

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual IEnumerator Flicker(float time = 0.1f)
    {
        rend.material.SetFloat("_FlickerFade", 1);
        yield return new WaitForSeconds(time);
        rend.material.SetFloat("_FlickerFade", 0);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public bool CheckWallBetween(Character toCheck, Vector3 fromPos)
    {
        Vector2 toCheckDir = toCheck.GetPosition() - fromPos;
        RaycastHit2D hit = Physics2D.Raycast(fromPos, toCheckDir, Vector2.Distance(fromPos, toCheck.GetPosition()), LayerMask.GetMask("wall"));
        return hit.collider;
    }

    public virtual Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Hit(Projectile projectile, out bool destroy)
    {
        projectile.gameObject.transform.parent = null;
        //Instantiate(sucHit, projectile.transform.position, Quaternion.identity);

        MoveCharacter(projectile.transform.position);

        Damage(projectile.damage);

        destroy = true;
    }

    public virtual void MoveCharacter(Vector3 by)
    {
        Vector3 characterDir = transform.position - by;
        characterDir = characterDir.normalized;
        transform.position += characterDir * 0.15f;
    }

    public virtual void AnimEvent()
    {

    }
}
