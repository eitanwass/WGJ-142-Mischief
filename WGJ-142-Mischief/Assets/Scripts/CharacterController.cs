using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    static protected CharacterController s_PlayerInstance;
    static public CharacterController PlayerInstance { get { return s_PlayerInstance; } }

    [SerializeField]
    private ParticleSystem dustCloud;

    public Rigidbody2D rb;
    public const float DEFAULT_SPEED = 5;
    public const float RUNNING_SPEED = DEFAULT_SPEED + 3;
    public float speed = DEFAULT_SPEED;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    /*private void Update()
    {
        
    }*/

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

        //check if shift is pressed (shift is the key for running)
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = RUNNING_SPEED;
        }
        else
        {
            speed = DEFAULT_SPEED;
        }

        System.Console.WriteLine("speed: " + speed);

        Vector2 velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        velocity *= speed;

        Debug.Log(velocity.ToString());
        if (velocity.x != 0 || velocity.y != 0)
        {
            StartDustParticles();
        }
        else
        {
            StopDustParticles();
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    public void StartDustParticles()
    {
        if (!dustCloud.isPlaying)
            dustCloud.Play();
    }

    public void StopDustParticles()
    {
        dustCloud.Stop();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collision.collider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool top = contactPoint.y > center.y;
    }
}
