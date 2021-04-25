using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AdvDropPick : MonoBehaviour
{
    [Header("               PICK UP DROP LOGIC")]
    [Header("                   DROP PICK UP SETTINGS")]
    public KeyCode dropWeapon = KeyCode.Q;
    public KeyCode pickWeapon = KeyCode.E;

    [HideInInspector] public GameObject gun;                // used for dropping the gun

    public WeaponHotbar weaponHotbar;
    void Start()
    {
        weaponHotbar = gameObject.GetComponent<WeaponHotbar>();
    }

    public void Update()
    {
        // DON'T Drop
        /*if (Input.GetKeyDown(dropWeapon) && Hands.handsfull == true)
        {
            Drop();
        }*/
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        // Picks Up Gun
        if (col.tag == "gun" && Input.GetKey(pickWeapon))
        {
            Pick(col);
        }
    }

    public void Drop()
    {
        gun.gameObject.GetComponent<GunDropAni>().dropWeapon();
        gun.gameObject.GetComponent<GunDropAni>().droppedBool = true;
        // Drops gun hands are no longer full
        gun.transform.parent = null;


        //Disables gun from following/shooting
        gun.GetComponent<GunRotation>().enabled = false;
        //gun.GetComponent<AdvGunLogic>().enabled = false;

        gun.GetComponent<SpriteRenderer>().sortingLayerName = "Moving";

        //Updates visuals including ui
        gun.transform.rotation = Quaternion.identity;
        GameObject.Find("UiManger").GetComponent<UiGun>().ammoClipSet(0, 0);
    }

    public void Pick(Collider2D col)
    {
        col.gameObject.GetComponent<GunDropAni>().pickWeapon();
        col.gameObject.GetComponent<GunDropAni>().droppedBool = false;

        //Moves guns to hand and makes child of player
        col.transform.position = transform.position;
        col.transform.parent = Player.instance.transform;

        col.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

        // Sets gun to what it collided with
        //gun = col.gameObject;

        //  Turns on gun
        col.GetComponent<GunRotation>().enabled = true;

        Item _item = col.GetComponent<Item>();
        if (_item) _item.enabled = true;
        //col.GetComponent<AdvGunLogic>().enabled = true;

        weaponHotbar.AddWeapon(col.gameObject);
    }
}