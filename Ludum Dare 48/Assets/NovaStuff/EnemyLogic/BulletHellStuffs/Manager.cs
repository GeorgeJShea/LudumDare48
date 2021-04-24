using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Reponsiable for bullet hell mechanics
    // keep track of player and if it can see it

    [Header("   Enemy Information")]
    [Tooltip("Increasing The level will use forumals to make them stronger")]
    public int lvl = 1;
    // current leveling forumlas

    public int agroRange;
    public int health;
    public int bulletDamage;
    public int bulletSpeed;
    // Agression 
    [Tooltip("How Often it shoots")]
    public float coolDown;
    private float coolDownReset;

    public GameObject[] patterns;

    private GameObject pivot;
    [HideInInspector] public GameObject player;

    public LayerMask layerMask;

    // ignore this bit its ued for math

    void Start()
    {
        StartUp();
    }

    void Update()
    {
        ManagerLogic();
    }
    public void StartUp()
    {
        //Increase Damage and other stats acording to level 
        if (lvl != 1)
        {
            AgroCalc(agroRange);
            HealthCalc(health);
            SpeedCalc(bulletSpeed);
            DamageCalc(bulletDamage);
        }
        //Gets child 
        pivot = transform.GetChild(0).gameObject;
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");

        //Setups timer
        coolDownReset = coolDown;
    }    

    public void ManagerLogic()
    {
        //Points pivot towards the player
        pivot.transform.LookAt(player.transform, Vector2.right);

        if (coolDown <= 0)
        {
            //Defines raycast will ingore anything other than walls and player
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, (player.transform.position - transform.position), agroRange, layerMask);
            if (hitInfo.point != new Vector2(0, 0))
            {
                //The above check is to make sure it actually hiting something
                if (hitInfo.transform.tag != "Player")
                {
                    // Will only shoot if have line of site o the player
                    coolDown = 0;
                }
                else
                {
                    //Picks pattern at random spawns it in and turns it into a child
                    GameObject temp = Instantiate(patterns[Random.RandomRange(0, patterns.Length)], transform.position, transform.localRotation);
                    temp.transform.parent = gameObject.transform;

                    //Resets timer
                    coolDown = coolDownReset;
                }
            }
            else
            {
                // Resets cooldown if either out of agro range or ray hit a wall
                coolDown = 0;
            }
        }
        else
        {
            // Timer
            coolDown -= Time.deltaTime;
        }
    }
    public int AgroCalc(float agro)
    {
        // how quickly it will attack
        // get precentage of increase
        float value = (-1 / 5 * lvl + 100) /100;
        // increase damage acorrding to lvl boost
        agro = agro * value;

        // turns that into an int to keep it clean
        int package = (int)Mathf.Round(agro);

        return package;
    }
    public int HealthCalc(float health)
    {
        float e = 2.7182f;
        // get precentage of increase
        float value = 7.51f * Mathf.Pow(e, (.03f * lvl)) / 100;
        // increase damage acorrding to lvl boost
        health = health + (health * value);

        // turns that into an int to keep it clean
        int package = (int)Mathf.Round(health);

        return package;
    }
    public int SpeedCalc(float speed)
    {
        // get precentage of increase
        float value = (1/2 * lvl + 5 )/ 100;
        // increase damage acorrding to lvl boost
        speed = speed + (speed * value);

        // turns that into an int to keep it clean
        int package = (int)Mathf.Round(speed);


        return package;
    }
    public int DamageCalc(float damage)
    {
        float e = 2.7182f;
        // get precentage of increase
        float value = 7.51f * Mathf.Pow(e, (.03f * lvl)) / 100;
        // increase damage acorrding to lvl boost
        damage = damage + (damage * value);

        // turns that into an int to keep it clean
        int package = (int)Mathf.Round(damage);


        return package;
    }
}
