﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    public Rigidbody2D rb;
    public float playerSpeed;
    public float jumpForce;

    public float fallMultiplier;
    public float lowJumpMultiplier;
    [SerializeField] bool isGrounded = true;
    private float jumpTimer = -1;

    private void Update()
    {
        if(jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;

        }

    }

    // Update is called once per frame
    void FixedUpdate () 
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.Translate(moveHorizontal, 0, 0);

        // Jump
        if (Input.GetButton("Jump") && isGrounded &&  jumpTimer < 0)
        {
            isGrounded = false;
            jumpTimer = .25f;
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

        groundCheck();
        
    }

    void groundCheck()
    {
        if(isGrounded == false)
        {
            return;

        }

        var raycastHits = Physics2D.RaycastAll(transform.position, Vector2.down, .75f);
        bool foundGround = false;

        for (var count = 0; count < raycastHits.Length; count++)
        {

            if (raycastHits[count].transform.tag == "Ground")
            {
                foundGround = true;

                break;

            }

        }

        if(foundGround == false)
        {
            isGrounded = false;

        }

    }

    // Jump Limiter
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded |= collision.gameObject.tag == "Ground";

    }
}