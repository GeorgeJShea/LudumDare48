using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Ai
{
    public float JumpSpeed;
    public bool Move;

    public AudioClip[] JumpSounds;

    public override void Awake()
    {
        base.Awake();

        anim.speed = Random.Range(0.9f, 1.1f);
    }

    protected override void Update()
    {
        if (isDead) return;

        base.Update();

        if (isAttacking)
        {
            Move = false;
            Agent.speed = 0.01f;
        }
    }

    public override void AnimEvent()
    {
        base.AnimEvent();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            Move = !Move;

            if (Move)
            {
                SoundManager.instance.PlaySound(JumpSounds[Random.Range(0, JumpSounds.Length)], Movement.position, 1);
            }

            Agent.speed = Move ? JumpSpeed : 0.01f;
        }
    }
}
