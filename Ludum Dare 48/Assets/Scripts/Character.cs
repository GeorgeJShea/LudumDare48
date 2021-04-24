using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHitable
{
    public float StartHealth = 100;

    public Renderer rend;

    public float Health { get { return health; } set { health = value; } }
    protected float health;

    public virtual void Awake()
    {
        health = StartHealth;
    }

    public virtual void Damage(float damage)
    {
        StartCoroutine(Flicker());
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

    public virtual void Hit(Projectile projectile, out bool destroy)
    {
        projectile.gameObject.transform.parent = null;
        //Instantiate(sucHit, projectile.transform.position, Quaternion.identity);
        Damage(projectile.damage);

        destroy = true;
    }

    public virtual void AnimEvent()
    {

    }
}
