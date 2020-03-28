using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    static protected CharacterController s_PlayerInstance;
    static public CharacterController PlayerInstance { get { return s_PlayerInstance; } }
    public float walkingSpeed = 5;
    public float runningSpeed = 8;

    private float speed;

    [SerializeField]
    private ParticleSystem dustCloud;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePosition = mainCam.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
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
        Debug.Log(dustCloud.isPlaying + " " + dustCloud.ToString());
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
