/*
 _______________________________________________________
 Summary:
Responsible for the firing logic of the gun as well as having options to edit the bullets


Updated on 28/9/2020
 
 _______________________________________________________
 */

using UnityEngine;

public class AdvGunLogic : Item
{
    [Header("               GUN SETTINGS")]
    [Header("                   GENERIC WEAPON SYSTEM")]

    [Tooltip("Time how often the player can pull the trigger")]
    public float triggerDelay;
    private float triggerDelayReset;

    [Tooltip("Time how long it takes to reload the gun")]
    public float reloadTime;

    [Header("")]
    [Tooltip("How expensive a gun is to shoot")]
    public float costToShoot;

    [Header("")]
    [Tooltip("How many bullets are currently in clip")]
    public float clip;
    [Tooltip("Clips max size")]
    public float clipSize;

    [Tooltip("Amount of ammo available for the gun to use")]
    public float ammoPool;

    private GameObject hands;

    //________________________________________________________________________
    [Header("               BURST SETTINGS")]
    [Header("")]

    [Tooltip("Put particle for muzzle flash here")]
    public GameObject muzzleFlash;

    private bool burstBool = false;     // Bool used for toggleing burst fire 

    [Tooltip("Amount of bullets per mouse click")]
    public float bullets;
    private float bulletsReset;

    [Tooltip("Amount bullets will spread")]
    public float deviation;


    [Tooltip("Time inbetween bursted bullets")]
    public float lagTime;
    private float lagTimeReset;
    private bool toFast = true;     // used for shotguns to prevant to much particle spam
    //________________________________________________________________________

    [Header("               BULLET SETTINGS")]
    [Header("")]

    [Tooltip("Put bullet here")]
    public GameObject bullet;
    [Tooltip("Gun noise")]
    public AudioSource gunNoise;
    [Tooltip("Bullet noise")]
    public AudioSource bulletNoise;
    [Header("")]

    [Tooltip("How much damage the bullet will do")]
    public float damage;
    [Tooltip("How fast the bullet will travel")]
    public float speed;
    [Tooltip("How long the bullet will remain on screen")]
    public float bulletLife;

    [HideInInspector]
    public UiGun uiComponent;    //Makes refrence to ui script

    void Start()
    {
        //Gun setup
        triggerDelayReset = triggerDelay;
        bulletsReset = bullets;
        lagTimeReset = lagTime;

        uiComponent = GameObject.Find("UiManger").GetComponent<UiGun>();
        hands = GameObject.Find("hands");
    }

    public void Update()
    {
        //Trigger cooldown once the cooldown runs out the user can click again
        if (triggerDelay < 0)
        {
            // sets ui
            uiComponent.ammoClipSet(clip, ammoPool);

            if (Input.GetKey(KeyCode.Mouse0) && clip - costToShoot >= 0)
            {
                // trigger delay ui
                uiComponent.ReloadAni(triggerDelayReset);

                //turns on burst mode
                burstBool = true;
            }
        }
        else
        {
            triggerDelay -= Time.deltaTime;
        }

        // Prevents bursting when not time.
        if (bullets >= 0 && lagTime < 0 && burstBool == true)
        {
            Burst();
        }
        else
        {
            lagTime -= Time.deltaTime;
        }

        //Reloads gun if less then required clip will fill with what remains
        Reload();
    }

    /*  Summary:
     Burst fire is how shotguns and burst fire guns opperate for this script.
    To make a shotgun bring up bullets count and reduce the lagtime to less then .1

    for a generic burst fire rifle bullets count from around 3-5 and set the burst to around .2 to .5 depending on what you want

    for a single shot gun just set bullets to 1 
    */

    public void Burst()
    {
        if (lagTimeReset < .1 && toFast == true)
        {
            Shoot();

            toFast = false;
        }

        //Goes up to player and then goes down to hand
        float angle = hands.transform.localEulerAngles.z + Random.Range(-deviation, deviation);
        // Sets the devation to this variable
        Quaternion bulletDev = Quaternion.Euler(new Vector3(0, 0, angle));

        //Bullet pooling
        GameObject bul = BulletPooling.bulletPoolInstance.GetBullet();
        bul.transform.position = new Vector3(transform.position.x, transform.parent.position.y);
        //bul.transform.rotation = bulletDev;
        bul.SetActive(true);

        bul.GetComponent<Projectile>().bulletSet(bulletNoise, damage, speed, bulletDev * Vector2.right, bulletLife, false, transform.root.gameObject, transform.localPosition.y);

        // Used to make sure that a ton of particles dont spawn
        if (lagTimeReset >= .1 && toFast == true)
        {
            Shoot();
        }

        //resets burst timer and removes bullet
        lagTime = lagTimeReset;
        bullets -= 1;

        if (bullets == 0)
        {
            //Resets stuff
            toFast = true;
            burstBool = false;

            bullets = bulletsReset;

            triggerDelay = triggerDelayReset;

            clip -= costToShoot;

            // updates ui
            GameObject.Find("UiManger").GetComponent<UiGun>().ammoClipSet(clip, ammoPool);
        }
    }

    /*  Summary:
        Will trigger inside of burst script responsible for instanting the bullet, the muzle flash and the noise.
    */
    public void Shoot()
    {
        Instantiate(muzzleFlash, transform.GetChild(0).transform.position, Quaternion.identity);
        if (gunNoise != null)
        {
            gunNoise.Play(0);
        }
        else
        {
            Debug.Log("You dont have a gun noise plugged in");
        }
    }

    /*  Summary:
        Simply reload the gun once empty prevent overload/ snagging bullets from no where.
    */
    public void Reload()
    {
        if (ammoPool > 0 && ammoPool < clipSize && clip == 0)
        {
            //Begins reload animation of ui
            uiComponent.ReloadAni(reloadTime);

            clip = ammoPool;
            ammoPool = 0;
            triggerDelay = reloadTime;
            uiComponent.ammoClipSet(clip, ammoPool);
        }
        else if (clip == 0 && ammoPool - clip > 0)
        {
            //Begins reload animation of ui
            uiComponent.ReloadAni(reloadTime);

            clip = clipSize;
            ammoPool -= clipSize;

            if (ammoPool < 0)
            {
                ammoPool = 0;
            }
            // Sets ui up to show current ammo amounts
            uiComponent.ammoClipSet(0, ammoPool);

            triggerDelay = reloadTime;
        }

    }
    /*
        simply adds ammo to gun
    */
    public void AmmoAdd(float pickUp)
    {
        // adds clip size * pickup mutipler
        ammoPool += clipSize * pickUp;
    }
}