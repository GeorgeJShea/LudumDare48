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

    public GameObject Graphics;

    // Dirived from manager script
    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public bool destroy;
    [HideInInspector] public GameObject shotBy;
    [HideInInspector] public bool flames;

    [HideInInspector] public Vector3 MoveDir;
    private float bulletHeight;
    private float shotTime;

    //[HideInInspector] public GameObject me;
    [HideInInspector] public SpriteRenderer partical;

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

        partical = gameObject.GetComponent<SpriteRenderer>();
    }

    public void bulletSet(AudioSource _bulletNoise, float _damage, float _speed, Vector3 _dir, float _bulletLife, bool _destroy, GameObject _shotBy, float _bulletHeight)
    {
        bulletNoise = _bulletNoise;
        damage = _damage;
        speed = _speed;
        lifeTime = _bulletLife;
        lifeTimeReset = _bulletLife;
        destroy = _destroy;
        shotBy = _shotBy;
        MoveDir = _dir;
        Graphics.transform.localPosition = new Vector3(0, _bulletHeight);
        bulletHeight = _bulletHeight;
        shotTime = Time.time;
        Graphics.transform.right = _dir;
        Graphics.GetComponent<SpriteSorting>().SorterPositionOffset = new Vector3(0, -_bulletHeight);
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
            if (shotBy)
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
        else
        {
            DestroyProjectile();
        }
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
        bulletSet(null, _damage, _speed, pattern * Vector2.right, lifeTime, true, _shotBy, 0);

        // Temp is the spawned in bullet 
        // Spawned from patterns script
        // Fixs weird scale as it is a child
        transform.localScale = new Vector3(.4f, .4f, 0);

        // Set it to the propper rotation
        /*
        pattern.z = -90;
        pattern.y = 0;
        pattern.x = pattern.x;
        */
        //transform.localRotation = pattern;

        //lifeTimeReset = lifeTime;
    }

    public void BulletLogic()
    {
        // Moves bullet forward in the direction of the blue arrow
        if (gameObject != null)
        {
            transform.Translate(MoveDir * speed * Time.deltaTime);
            Graphics.transform.localPosition = new Vector3(0, Mathf.Lerp(bulletHeight, 0, Time.time - shotTime), 0);
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
