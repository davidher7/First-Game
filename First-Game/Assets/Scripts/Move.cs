using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour 
{
    public Rigidbody2D rb;
    private float playerSpeed = 7;
    private float jumpForce = 8;

    private float fallMultiplier = 3.5f;
    private float lowJumpMultiplier = 2.25f;
    bool isGrounded;

    float groundCheckDistance = .5f;

    private void Start()
    {
        LoadPlayerPos();

    }

    // Update is called once per frame
    void FixedUpdate () 
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.Translate(moveHorizontal, 0, 0);

        // Jump
        if (Input.GetButton("Jump") && isGrounded)
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

        GroundCheck();
        
    }

    void GroundCheck()
    {
        if(isGrounded == false)
        {
            return;

        }

        var raycastHits = Physics2D.RaycastAll(transform.position, Vector2.down, groundCheckDistance);
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

    // Resets isGrounded to true 
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded |= collision.gameObject.tag == "Ground";

    }

    public void LoadPlayerPos()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (!PlayerPrefs.HasKey(sceneIndex + "PlayerX"))
        {
            return;

        }
        
        // Loads Player's position
        transform.position = new Vector2(PlayerPrefs.GetFloat(sceneIndex + "PlayerX"), PlayerPrefs.GetFloat(sceneIndex + "PlayerY"));

        Debug.Log("Load");

    }

    // Draws groundCheckDistance 
    private void OnDrawGizmos()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.blue;

        }
        else
        {
            Gizmos.color = Color.red;

        }

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

    }
}
