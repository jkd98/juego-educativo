using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour
{
    //referencia al joystick
    public Joystick joystick;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    public float runSpeed = 2.0f;
    public float jumpSpeed = 3.0f;
    public float doubleJumpSpeed = 2.5f;
    private bool canDoubleJump = false;
    Rigidbody2D rb2D; // Referencia al componente


    // Se hace public, para poder arrastrarlo desde el editor
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalMove > 0)
        {
            // Para moverse a la derecha
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);

        }
        else if (horizontalMove < 0)
        {
            //Para moverse a la izquierda
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);

        }
        else
        {
            animator.SetBool("Run", false);
        }

        //***Para controlar la animación de salto 
        // Cuando no estamos en el suelo
        if (CheckGround.isGrounded == false)
        {
            //Estamos saltando
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
            Debug.Log("EN AIRE - No puede saltar");
        }
        if (CheckGround.isGrounded)
        {
            //Estamos en el suelo
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }
        //**

        if (rb2D.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb2D.velocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }
    }
    void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * runSpeed;
        rb2D.velocity = new Vector2(horizontalMove, rb2D.velocity.y); // Mantiene la velocidad Y actual
    }

    public void Jump()
    {
        if (CheckGround.isGrounded)
        {
            canDoubleJump = true; // Resetea el doble salto al estar en el suelo
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
        else
        {
            if (canDoubleJump)
            {
                animator.SetBool("DoubleJump", true);
                rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                canDoubleJump = false; // Se gasta el doble salto

            }
        }
    }
}
