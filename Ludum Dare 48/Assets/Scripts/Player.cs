using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
