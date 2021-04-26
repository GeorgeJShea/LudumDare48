using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IHitable
{
    public SpriteRenderer rend;
    public Sprite[] Sprites;

    public GameObject[] ToSpawn;

    public float Health { get { return 1; } set { } }

    private void Awake()
    {
        rend.sprite = Sprites[Random.Range(0, Sprites.Length)];
    }

    public void Damage(float damage, bool makeHitSound = true)
    {
        if (ToSpawn.Length > 0) Instantiate(ToSpawn[Random.Range(0, ToSpawn.Length)], transform.position + (Vector3.up * 0.5f), Quaternion.identity);

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
