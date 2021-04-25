using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    [Header("                   GENERIC WEAPON SYSTEM")]

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

    [Header("")]
    [Tooltip("Gun noise")]
    public AudioSource gunNoise;
    [Tooltip("Bullet noise")]
    public AudioSource bulletNoise;
    [Header("")]

    [Tooltip("How much damage the bullet will do")]
    public float damage;

    [HideInInspector]
    public UiGun uiComponent;    //Makes refrence to ui script

    void Start()
    {
        uiComponent = GameObject.Find("UiManger").GetComponent<UiGun>();
        hands = GameObject.Find("hands");
    }

    public void Update()
    {
        uiComponent.ammoClipSet(0, ammoPool);
    }
}
