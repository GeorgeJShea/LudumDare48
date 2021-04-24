using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    public int clipMultipler = 2;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "gun")
        {
            col.GetComponent<AdvGunLogic>().AmmoAdd(clipMultipler);
            Destroy(gameObject);
        }
    }

}
