using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Transform gun;
    public float AngleOffset;

    private void Start()
    {

    }
    void Update()
    {
        AngleOffset = Mathf.Lerp(AngleOffset, 0, 3.5f * Time.deltaTime);

        Vector3 diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diffrence.Normalize();
        float rotz = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + AngleOffset);

        if (rotz < -90 || rotz > 90)
        {
            if (gun.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotz + AngleOffset);
            }
            else if (gun.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotz + AngleOffset);
            }
        }
    }
}