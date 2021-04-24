using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour, IHitable
{
    public NavMeshAgent Agent;

    public float Health { get { return health; } set { health = value; } }
    protected float health;

    public void Damage(float damage)
    {

    }

    public void Hit(Projectile projectile)
    {
        projectile.gameObject.transform.parent = null;
        //Instantiate(sucHit, projectile.transform.position, Quaternion.identity);
        Destroy(projectile.gameObject);
    }
}
