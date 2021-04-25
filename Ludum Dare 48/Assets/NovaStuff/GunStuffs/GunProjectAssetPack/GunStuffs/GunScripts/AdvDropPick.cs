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

    public Transform WeaponsParent;
    public WeaponHotbar weaponHotbar;

    public GameObject[] WeaponsToPickUp;

    void Start()
    {
        weaponHotbar = gameObject.GetComponent<WeaponHotbar>();

        foreach (var g in WeaponsToPickUp)
        {
            Pick(g);
        }

        weaponHotbar.HoldWeapon(0);
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
            Pick(col.gameObject);
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

    public void Pick(GameObject item)
    {
        item.gameObject.GetComponent<GunDropAni>().pickWeapon();
        item.gameObject.GetComponent<GunDropAni>().droppedBool = false;

        //Moves guns to hand and makes child of player
        item.transform.position = transform.position;
        item.transform.parent = WeaponsParent;

        item.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

        // Sets gun to what it collided with
        //gun = col.gameObject;

        //  Turns on gun
        item.GetComponent<GunRotation>().enabled = true;

        Item _item = item.GetComponent<Item>();
        if (_item) _item.enabled = true;
        //col.GetComponent<AdvGunLogic>().enabled = true;

        weaponHotbar.AddWeapon(item.gameObject);
    }
}