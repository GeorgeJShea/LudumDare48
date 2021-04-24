using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveVelocity;

    public float speed = 20.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveVelocity = new Vector2(moveX, moveY).normalized;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveVelocity.x * speed, moveVelocity.y * speed);
    }
}
