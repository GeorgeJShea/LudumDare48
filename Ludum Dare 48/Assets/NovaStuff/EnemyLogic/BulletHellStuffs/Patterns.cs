using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patterns : MonoBehaviour
{
    /*
     will set up patterns that bullets will be slotting into  
     
     */
    private Manager manager;
    private Projectile projectile;
    [Tooltip("Project to be shot")]
    [Header("                   SETTINGS")]
    public GameObject bulletType;

    //[Tooltip("True will go frop left to right, and false will go right to left ")]
    //public bool topOrDown;

    [Header("                   ")]
    [Tooltip("Amount of bullets spawned")]
    public int bulletCount;
    private int bulletCountReset;

    [Tooltip("Range that bullets will spawn")]
    public int angleSpread;

    [Tooltip("Add offset to where the player is")]
    public int offset;

    // Passed into bullet once spawned in
    [HideInInspector] public int damage;
    [HideInInspector] public float bulletSpeed;


    [Header("                   ")]
    public float coolDown;
    private float coolDownReset;

    // Calculated dynamicly 
    private int increment;

    private float x;
    private float y;

    // leveling formulas
    //https://www.desmos.com/calculator/p4b9skecma

    public void Start()
    {
        PatternSet();
    }

    void Update()
    {
        Volly();
    }
    public void Volly()
    {
        // first volley
        if (bulletCount >= 0 && coolDown <= 0)
        {
            // Rotates to the next angle
            transform.localRotation *= Quaternion.Euler(-increment, 0, 0);

            // Shoots a bullet sets it to be a child, sets propper rotation and finally passes in relavent info
            GameObject temp = Instantiate(bulletType, transform.position, transform.localRotation);
            temp.transform.parent = transform.parent;
            temp.GetComponent<Projectile>().IAmAlive(damage, bulletSpeed, temp, transform.localRotation);

            //Preps for next volly
            coolDown = coolDownReset;
            bulletCount -= 1;
        }
        else
        {
            //Timer
            coolDown -= Time.deltaTime;
        }
        if (bulletCount == 0)
        {
            // End point of pattern 
            bulletCount = bulletCountReset;
            Destroy(gameObject);
        }
    }    
    public void PatternSet()
    {
        //Finds Pivot which has the manager script attacted
        manager = transform.parent.GetComponent<Manager>();
        // Sets Cooldown of manager to allow for the pattern to play out in full. 
        manager.coolDown = manager.coolDown + coolDown * bulletCount;

        //Setting
        int dmg = manager.bulletDamage;
        int speed = manager.bulletSpeed;

        projectile = bulletType.GetComponent<Projectile>();

        // set this to that
        transform.rotation = transform.parent.GetChild(0).localRotation;

        // Gets starting angle of pattern adding in offset. 
        float initialRot = (angleSpread / 2) + offset;
        
        // Moves pattern spawner to be in line with pivot
        // which means it pointing to the player 
        transform.localRotation *= Quaternion.Euler(initialRot, 0, 0);

        // Does lvl calculations to increase the damage of the bullets and their speed
        damage = manager.DamageCalc(dmg);
        bulletSpeed = manager.SpeedCalc(speed);

        //Angle between bullets
        increment = angleSpread / bulletCount;

        //Setsup up variables for pattern
        coolDownReset = coolDown;
        bulletCountReset = bulletCount;

        //Used to set bullets in the propper direction
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }
}
