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

    public AudioSource bulletNoise;

    // Dirived from manager script
    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public bool destroy;
    [HideInInspector] public GameObject shotBy;

    //[HideInInspector] public GameObject me;

    private void Awake()
    {
        //Plays bullet noise
        if (bulletNoise != null)
        {
            bulletNoise.Play();
        }
        else
        {
            Debug.Log("Bullet lacking sound");
        }
    }

    public void bulletSet(AudioSource _bulletNoise, float _damage, float _speed, float _bulletLife, bool _destroy, GameObject _shotBy)
    {
        bulletNoise = _bulletNoise;
        damage = _damage;
        speed = _speed;
        lifeTime = _bulletLife;
        lifeTimeReset = _bulletLife;
        destroy = _destroy;
        shotBy = _shotBy;
    }

    public void Update()
    {
        //Starts movement, also destroys and spawns in affects.
        BulletLogic();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hitable = collision.GetComponentInParent<IHitable>();

        if (hitable != null)
        {
            if(shotBy)
            {
                if (collision.transform.IsChildOf(shotBy.transform))
                {
                    return;
                }
            }

            hitable.Hit(this, out bool _destroy);
            if (_destroy)
            {
                DestroyProjectile();
            }
        }
        /*else
        {
            DestroyProjectile();
        }*/
    }

    public void DestroyProjectile()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void IAmAlive(int _damage, float _speed, Quaternion pattern, GameObject _shotBy)
    {
        bulletSet(null, _damage, _speed, lifeTime, true, _shotBy);

        // Temp is the spawned in bullet 
        // Spawned from patterns script
        // Fixs weird scale as it is a child
        transform.localScale = new Vector3(.4f, .4f, 0);

        // Set it to the propper rotation
        pattern.z = pattern.x;
        pattern.y = 0;
        pattern.x = 0;
        transform.localRotation = pattern;

        //lifeTimeReset = lifeTime;
    }

    public void BulletLogic()
    {
        // Moves bullet forward in the direction of the blue arrow
        if (gameObject != null)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        // Lifetime countdown
        if (lifeTime <= 0)
        {
            if (null != petterOut)
            {
                // Instates an affect if bullet petters out
                Instantiate(petterOut, transform.position, Quaternion.identity);
            }
            DestroyProjectile();
            //Destroy(gameObject);

            // resets the timer
            lifeTime = lifeTimeReset;
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }
}
