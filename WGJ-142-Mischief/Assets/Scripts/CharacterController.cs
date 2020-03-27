using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal == 0 && vertical == 0)
        {
            // Not moving
        }

        Vector2 velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        velocity *= speed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collision.collider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool top = contactPoint.y > center.y;
    }
}
