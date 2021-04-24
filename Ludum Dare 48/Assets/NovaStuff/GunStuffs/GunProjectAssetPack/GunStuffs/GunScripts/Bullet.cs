using UnityEngine;

public class Bullet : MonoBehaviour
{
    //All bullet stats get over ridden by guns
    public AudioSource bulletNoise;

    private Rigidbody2D rb;
    private float speed = 7f;
    private float damage = 1f;
    private float bulletTime = 3f;
    private int pierceNumb = 1;
    private bool explosive = false;
    private float explosiveRange;
    private GameObject explosiveAffect;
    private float explosiveDamage;

    private Collider2D nullcol2D; //used when is not exploding


    
    void Awake()
    {
        //Setup
        LayerMask hitMask = LayerMask.GetMask("enemy");
        rb = GetComponent<Rigidbody2D>();
        nullcol2D = rb.gameObject.GetComponent<Collider2D>();

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

    // Update is called once per frame
    void Update()
    {
        //Simple lifetime timer
        if (bulletTime >= 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            bulletTime -= Time.deltaTime;
        }
        else if (bulletTime < 0)
        {
            //Sets kablamm to self 
            Collider2D nullCollider = rb.gameObject.GetComponent<Collider2D>();
            Kablaam(nullcol2D);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // if enenemy do damage. 
        if (col.tag == "enemy")
        {
            col.GetComponent<EnemyHealth>().Damage(damage);

            pierceNumb -= 1;

            if (pierceNumb <= 0)
            {
                Kablaam(col);
            }
        }
        if (col.tag == "wall")
        {
            Kablaam(nullcol2D);
        }
    }
    //Responsible for destroying bullets
    public void Kablaam(Collider2D col)
    {
        if (explosive == true)
        {
            //Spawns in explosion affect
            Instantiate(explosiveAffect, col.transform.position, Quaternion.identity);

            //creats raycast circle goes through it and deals damage specified
            RaycastHit2D[] hit; hit = Physics2D.CircleCastAll(col.transform.position, explosiveRange, Vector2.zero);
            foreach(RaycastHit2D i in hit)
            {
                if (i.transform.tag== "enemy")
                {
                    //goes through list and deals damage to them
                    i.transform.GetComponent<EnemyHealth>().Damage(explosiveDamage);
                }
            }
        }
        // turns bullet off used for bullet Pooling
        gameObject.SetActive(false);
    }

//Overide variables using AdvGunLogic 
    public void bulletSet(AudioSource bulletNoise, float damage, float speed, float bulletLife,
        int pierceNumb, bool explosive, GameObject explosiveAffect, float explosiveRange, float explosiveDm)
    {
        this.bulletNoise = bulletNoise;
        this.damage = damage;
        this.speed = speed;
        this.bulletTime = bulletLife;
        this.pierceNumb = pierceNumb;
        this.explosive = explosive;
        this.explosiveRange = explosiveRange;
        this.explosiveAffect = explosiveAffect;
        this.explosiveDamage = explosiveDm;
    }
}
