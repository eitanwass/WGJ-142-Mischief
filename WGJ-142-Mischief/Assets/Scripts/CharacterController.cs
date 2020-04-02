using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody2D rb;

    public float walkingSpeed = 3;
    public float runningSpeed = 5;

    private float speed;
    private Animator animator;


    private Vector3 mousePosition;


    private void Awake()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePosition = mainCam.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
    }

    private void FixedUpdate()
    {
        if (!PauseMenu.isPaused)
        {
            Vector2 velocity = HandleMovement();
            animator.SetBool("Moving", velocity != Vector2.zero);
            animator.SetBool("Up", velocity.y > 0);
            transform.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
        }

        
    }


    private Vector2 HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0)
        {
            // Not moving
            animator.SetBool("Moving", false);
            return Vector2.zero;
        }
        else
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && StaminaBar.instance.getStamina() > 0)
            {
                speed = runningSpeed;
                StaminaBar.instance.UseStamina(1);
            }
            else
                speed = walkingSpeed;

            Vector2 velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            velocity *= speed;

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            return velocity;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collision.collider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool top = contactPoint.y > center.y; 
    } */
}
