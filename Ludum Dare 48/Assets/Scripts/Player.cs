using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player instance;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
