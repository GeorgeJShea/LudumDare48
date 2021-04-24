using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("How Long The bullet will live")]
    [Header("       SETTINGS")]
    public float lifeTime;
    private float lifeTimeReset;

    [Tooltip("Place bullet destruction affect")]
    public GameObject petterOut;
    public GameObject sucHit;

    // Dirived from manager script
    [HideInInspector] public int damage;
    [HideInInspector] public float speed;

    [HideInInspector] public GameObject me;



    public void Update()
    {
        //Starts movement, also destroys and spawns in affects.
        BulletLogic();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hitable = collision.GetComponentInParent<IHitable>();

        if (hitable != null) hitable.Hit(this);
    }
    public void HurtPlayer(int damage)
    {
        //refrphace player health here and add amage to it
    }
    public void IAmAlive(int damage2, float speed2, GameObject temp, Quaternion pattern)
    {
        // Temp is the spawned in bullet 
        // Spawned from patterns script
        me = temp;
        // Fixs weird scale as it is a child
        temp.transform.localScale = new Vector3(.4f, .4f, 0);

        // Set it to the propper rotation
        pattern.z = pattern.x;
        pattern.y = 0;
        pattern.x = 0;
        temp.transform.localRotation = pattern;

        // Updates Bullet data
        damage = damage2;
        speed = speed2;

        // Sets up lifetime timer
        lifeTimeReset = lifeTime;
    }
    public void BulletLogic()
    {
        // Moves bullet forward in the direction of the blue arrow
        if (me != null)
        {
            me.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Lifetime countdown
        if (lifeTime <= 0)
        {
            if (null != petterOut)
            {
                // Instates an affect if bullet petters out
                Instantiate(petterOut, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);

            // resets the timer
            lifeTime = lifeTimeReset;
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }
}
