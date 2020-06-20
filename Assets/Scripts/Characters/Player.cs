using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float baseSpeed;
    public float maxSpeed;
    public float acceleration;
    public float jumpForce;
    public GameObject weapon;

    private Rigidbody2D player;
    private Vector3 localScale;
    private Vector3 weaponScale;
    private Animator animator;

    private bool  isGrounded, isJumped;
    private bool facingRight;

    private float dirX;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        weaponScale = weapon.transform.localScale;
        animator = GetComponent<Animator>();
        facingRight = true;
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Перемещение по горизонтали
        dirX = Input.GetAxis("Horizontal");     
        player.velocity = new Vector2(dirX * currentSpeed, player.velocity.y);
     
        //Прыжок
        if (Input.GetButtonDown("Jump") && !isJumped)
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            isJumped = true;
        }

        //Спринт
        animator.SetFloat("speed", Mathf.Abs(dirX * currentSpeed));

        if (Input.GetButton("Run") && currentSpeed < maxSpeed)
            currentSpeed += acceleration;
        else if (currentSpeed > baseSpeed)
            currentSpeed -= acceleration;
            
        //Перекат
        if (Input.GetButtonDown("Fire2"))
        {          
            animator.SetBool("isRolling", true);
            currentSpeed = maxSpeed;             
        }

           
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
        /*if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;
            
         if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;
         */

        //Расчет позиции мыши на экране
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Поворот оружия и персонажа
        float angle = Mathf.Atan2(mousePos.y - weapon.transform.position.y, mousePos.x - weapon.transform.position.x) * Mathf.Rad2Deg;

        //Debug.Log(angle);

        if ((Mathf.Abs(angle) < 70 && !facingRight) || (Mathf.Abs(angle) > 110 && facingRight)) 
        {
            localScale.x *= -1;
            weaponScale.x *= -1;
            weaponScale.y *= -1;

            facingRight = !facingRight;

            weapon.transform.localScale = weaponScale;
            transform.localScale = localScale;
        }

        weapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

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


    public void resetBool(string name)
    {
         animator.SetBool(name, false);
    }
            

}



