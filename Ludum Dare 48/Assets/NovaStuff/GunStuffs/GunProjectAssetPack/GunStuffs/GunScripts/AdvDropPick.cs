﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AdvDropPick : MonoBehaviour
{
    [Header("               PICK UP DROP LOGIC")]
    [Header("                   DROP PICK UP SETTINGS")]
    public KeyCode dropWeapon = KeyCode.Q;
    public KeyCode pickWeapon = KeyCode.E;

    private GameObject gun;                // used for dropping the gun
    
    private bool handsFull = false;      // used to prevant picking up multiple guns
    private GameObject hands;           // Used for setting gun in hands
    private GunDropAni GDA;

    void Start()
    {
        handsFull = Hands.handsfull;        //Hands script used to keep track of wether hands are full
        hands = GameObject.Find("hands");  // Finds players hands

    }

    public void Update()
    {
        //Drop
        if (Input.GetKeyDown(dropWeapon) && Hands.handsfull == true)
        {
            Drop();

        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        // Picks Up Gun
        if (col.tag == "gun" && Input.GetKey(pickWeapon) && Hands.handsfull == false)
        {
            Pick(col);
        }
    }

    public void Drop()
    {
        gun.gameObject.GetComponent<GunDropAni>().dropWeapon();
        gun.gameObject.GetComponent<GunDropAni>().droppedBool = true;
        // Drops gun hands are no longer full
        Hands.handsfull = false;
        gun.transform.parent = null;


        //Disables gun from following/shooting
        gun.GetComponent<GunRotation>().enabled = false;
        gun.GetComponent<AdvGunLogic>().enabled = false;

        //Updates visuals including ui
        gun.transform.rotation = Quaternion.identity;
        GameObject.Find("UiManger").GetComponent<UiGun>().ammoClipSet(0, 0);


    }

    public void Pick(Collider2D col)
    {
        col.gameObject.GetComponent<GunDropAni>().pickWeapon();
        col.gameObject.GetComponent<GunDropAni>().droppedBool = false;

        //Fills hands prevent picking up mutiple guns
        Hands.handsfull = true;

        //Moves guns to hand and makes child of player
        col.transform.position = GameObject.Find("player").transform.position;
        col.transform.parent = GameObject.Find("player").transform;

        // Sets gun to what it collided with
        gun = col.gameObject;

        //  Turns on gun
        col.GetComponent<GunRotation>().enabled = true;
        col.GetComponent<AdvGunLogic>().enabled = true;
    }
}