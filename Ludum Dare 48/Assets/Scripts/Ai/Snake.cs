using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Ai
{
    public float JumpSpeed;
    public bool Move;

    protected override void Update()
    {
        base.Update();

        if (isAttacking)
        {
            Move = false;
        }
    }

    public override void AnimEvent()
    {
        base.AnimEvent();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            Move = !Move;
            Agent.speed = Move ? JumpSpeed : 0;
        }
    }
}
