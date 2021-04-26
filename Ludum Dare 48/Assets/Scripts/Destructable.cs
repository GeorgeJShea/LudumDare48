using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IHitable
{
    public GameObject[] ToSpawn;

    public AudioClip BreakSound;

    public float Health { get { return 1; } set { } }

    public void Damage(float damage, bool makeHitSound = true)
    {
        if (ToSpawn.Length > 0) Instantiate(ToSpawn[Random.Range(0, ToSpawn.Length)], transform.position + (Vector3.up * 0.5f), Quaternion.identity);

        SoundManager.instance.PlaySound(BreakSound, transform.position, 1);

        GetComponent<Animator>().Play("BarrelExplode");

        Destroy(GetComponent<BoxCollider2D>());
        Destroy(this);
    }

    public virtual void Hit(Projectile projectile, out bool destroy)
    {
        projectile.gameObject.transform.parent = null;
        //Instantiate(sucHit, projectile.transform.position, Quaternion.identity);

        Damage(projectile.damage);

        destroy = true;
    }
}
