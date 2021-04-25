using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Ai
{
    public float JumpSpeed;
    public bool Move;

    public override void Awake()
    {
        base.Awake();

        anim.speed = Random.Range(0.9f, 1.1f);
    }

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
            Agent.speed = Move ? JumpSpeed : 0.01f;
        }
    }
}
