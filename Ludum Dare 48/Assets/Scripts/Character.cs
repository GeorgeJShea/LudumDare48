using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHitable
{
    public float StartHealth = 100;

    public float Health { get { return health; } set { health = value; } }
    protected float health;

    public virtual void Awake()
    {
        health = StartHealth;
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
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
}
