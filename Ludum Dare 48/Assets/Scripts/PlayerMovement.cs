using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveVelocity;

    public bool freezeInput;

    public float speed = 20.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!freezeInput)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveVelocity = new Vector2(moveX, moveY).normalized;
        }
    }
    private void FixedUpdate()
    {
        if (!freezeInput)
        {
            rb.velocity = new Vector2(moveVelocity.x * speed, moveVelocity.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
