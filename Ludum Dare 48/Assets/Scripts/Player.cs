using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Animator anim;

    private Rigidbody2D rb;

    public static Player instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }
    private static Player _instance;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("X", rb.velocity.x);

        if (rb.velocity.sqrMagnitude == 0)
        {
            anim.SetBool("IsMoving", false);
        }
        else
        {
            anim.SetBool("IsMoving", true);
        }
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public void MovePlayer(Ai by)
    {
        Vector3 characterDir = transform.position - by.Movement.position;
        characterDir = characterDir.normalized;
        transform.position += characterDir * 0.15f;
    }
}
