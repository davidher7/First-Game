using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    public Rigidbody2D rb;
    public float playerSpeed;
    public float jumpForce;

    public float fallMultiplier;
    public float lowJumpMultiplier;
    bool isGrounded = true;

    // Update is called once per frame
    void FixedUpdate () 
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.Translate(moveHorizontal, 0, 0);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.velocity += Vector2.up * jumpForce;

        }

        // Better Jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    // Jump Limiter
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded |= collision.gameObject.tag == "Ground";

    }
}
