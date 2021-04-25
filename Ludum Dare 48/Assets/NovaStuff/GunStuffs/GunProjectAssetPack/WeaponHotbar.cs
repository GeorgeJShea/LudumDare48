using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHotbar : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();

    public int currentWeapon = 0;

    public AdvDropPick DropPick;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    // Update is called once per frame
    void Update()
    {
        if (Weapons.Count == 0) return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentWeapon++;
            if (currentWeapon >= Weapons.Count) currentWeapon = 0;

            HoldWeapon(currentWeapon);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentWeapon--;
            if (currentWeapon < 0) currentWeapon = Weapons.Count - 1;

            HoldWeapon(currentWeapon);
        }
        else
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    if (i > -1 && i < Weapons.Count)
                    {
                        currentWeapon = i;
                        HoldWeapon(currentWeapon);
                    }
                }
            }
        }
    }

    public void HoldWeapon(int gunIndex)
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].SetActive(i == gunIndex);

            if (i == gunIndex) DropPick.gun = Weapons[i];
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        Weapons.Add(weapon);
        HoldWeapon(Weapons.Count - 1);
    }
}
