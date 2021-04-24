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
        //anim.SetFloat("X", rb.velocity.x);
        Vector3 mouseDir = CameraController.instance.cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        anim.SetFloat("X", mouseDir.x);

        anim.SetBool("IsMoving", rb.velocity.sqrMagnitude != 0);
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
