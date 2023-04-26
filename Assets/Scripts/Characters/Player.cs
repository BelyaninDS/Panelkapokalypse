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

    private bool isGrounded, isJumped, isClimbing;
    private bool facingRight;

    private float dirX;
    private float dirY;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        weapon.transform.SetParent(gameObject.transform);
        localScale = transform.localScale;
        weaponScale = weapon.transform.localScale;
       
        facingRight = true;
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Возвращение гравитации в нормальное значение после изменения (например, взаимодействие с лестницей)
        player.gravityScale = 1;

        //Перемещение по горизонтали
        dirX = Input.GetAxis("Horizontal");     
        player.velocity = new Vector2(dirX * currentSpeed, player.velocity.y);
     
        //Прыжок
        if (Input.GetButtonDown("Jump") && !isJumped)
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            isJumped = true;
        }

        //Бег
        animator.SetFloat("speed", Mathf.Abs(dirX * currentSpeed));

        if (Input.GetButton("Run") && currentSpeed < maxSpeed && isGrounded)
            currentSpeed += acceleration;
        else if (currentSpeed > baseSpeed && isGrounded)
            currentSpeed -= acceleration;
            
        //Перекат
        if (Input.GetButtonDown("Fire2"))
        {          
            animator.SetBool("isRolling", true);
            currentSpeed = maxSpeed;             
        }

        //Подъем по лестнице
        if (isClimbing)
        {
            InteractWithLadder();
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
    void LateUpdate()
    {
        //Расчет позиции мыши на экране
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Поворот оружия и персонажа
        float angle = Mathf.Atan2(mousePos.y - weapon.transform.position.y, mousePos.x - weapon.transform.position.x) * Mathf.Rad2Deg;


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
    //Взаимодействие с лестницами
    private void InteractWithLadder()
    {
        player.gravityScale = 0;
        dirY = Input.GetAxis("Vertical");
        player.velocity = new Vector2(0f, dirY);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isJumped = false;
        }

        if (other.gameObject.tag == "Platform")
        {
            if (isClimbing)
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
            else
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), false);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void resetBool(string name)
    {
         animator.SetBool(name, false);
    }


    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Ladder" & Input.GetButtonDown("Interact"))
        {
            //Притягивание игрока к лестнице 
            transform.position = new Vector2(other.gameObject.transform.position.x, transform.position.y);

            //Инверсия переменной isClimbing по взаимодействию
            if (isClimbing)
                isClimbing = false;
            else
                isClimbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isClimbing = false;
        }
                     
    }
}



