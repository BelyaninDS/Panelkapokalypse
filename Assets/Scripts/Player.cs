using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;
    private Vector3 localScale;
    private Animator animator;

    public float speed, jumpForce;

    private bool  isGrounded, isJumped;
    private bool facingRight;

    private float dirX;    


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        animator = GetComponent<Animator>();
        facingRight = true;      
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");

        player.velocity = new Vector2(dirX * speed, player.velocity.y);
        

        //Jumping
        if (Input.GetButtonDown("Jump") && !isJumped)
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            isJumped = true;
        }

        Debug.Log(isGrounded);
        

        if (Mathf.Abs(dirX) > 0.0001)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
        /*
        if (player.velocity.y >= 1)
            animator.SetBool("isJumping", true);
        else if (Mathf.Abs(player.velocity.y) <= 1)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isOnTop", true);
        }
        else if (player.velocity.y < -1)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
            animator.SetBool("isOnTop", false);
        }

        Debug.Log(player.velocity.y);
        */


    }

        private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isJumped = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}



