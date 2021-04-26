using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IHitable
{
    public SpriteRenderer rend;
    public Sprite[] Sprites;

    public float Health { get { return 1; } set { } }

    private void Awake()
    {
        rend.sprite = Sprites[Random.Range(0, Sprites.Length)];
    }

    public void Damage(float damage, bool makeHitSound = true)
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
