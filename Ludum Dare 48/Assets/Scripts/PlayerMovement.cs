using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveVelocity;

    public bool freezeInput { get; set; }

    public float speed = 20.0f;

    public float dashSpeed;
    public float dashDistance;
    public bool isDashing;
    public Animator anim;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(IDoDash());
            }
        }
    }
    private void FixedUpdate()
    {
        if (!freezeInput)
        {
            rb.velocity = new Vector2(moveVelocity.x * speed, moveVelocity.y * speed);
        }
    }

    public void FreezeInput()
    {
        freezeInput = true;
        rb.velocity = Vector2.zero;
    }

    private IEnumerator IDoDash()
    {
        Vector2 startPos = transform.position;

        isDashing = true;

        anim.Play("Hurt");
        freezeInput = true;

        rb.velocity = rb.velocity.normalized * dashSpeed;

        while (true)
        {
            //anim.Play("Hurt");

            anim.SetFloat("X", rb.velocity.normalized.x);

            if (rb.velocity.magnitude < dashSpeed * 0.5f || Vector2.Distance(transform.position, startPos) >= dashDistance)
            {
                break;
            }

            rb.velocity = rb.velocity.normalized * dashSpeed;

            yield return null;
        }
        isDashing = false;
        freezeInput = false;
    }
}
