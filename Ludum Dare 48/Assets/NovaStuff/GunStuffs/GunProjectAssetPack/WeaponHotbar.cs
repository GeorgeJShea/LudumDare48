using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHotbar : MonoBehaviour
{
    public GameObject weaponOne;
    public GameObject weaponTwo;
    public GameObject weaponThree;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            weaponOne.active = true;
            weaponTwo.active = false;
            weaponThree.active = false;
        }
        if (Input.GetKeyDown("2"))
        {
            weaponOne.active = false;
            weaponTwo.active = true;
            weaponThree.active = false;
        }
        if (Input.GetKeyDown("3"))
        {
            weaponOne.active = false;
            weaponTwo.active = false;
            weaponThree.active = true;
        }
    }
}
