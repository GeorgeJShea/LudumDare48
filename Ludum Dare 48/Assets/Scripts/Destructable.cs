using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IHitable
{
    public SpriteRenderer rend;
    public Sprite[] Sprites;

    public float Health { get { return health; } set { health = value; } }
    private float health;

    public void Damage(float damage, bool makeHitSound = true)
    {
        Destroy(gameObject);
    }

    public void Hit(Projectile projectile, out bool destroy)
    {
        throw new System.NotImplementedException();
    }
}
