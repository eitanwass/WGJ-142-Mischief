using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    static protected CharacterController s_PlayerInstance;
    static public CharacterController PlayerInstance { get { return s_PlayerInstance; } }

    private Camera mainCam;
    private Rigidbody2D rb;

    public float walkingSpeed = 5;
    public float runningSpeed = 8;

    private float speed;


    private Vector3 mousePosition;

    [SerializeField]
    public ParticleSystem dustCloud;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (mainCam)
            mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
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
        else
        {
            StopCoroutine("StopParticle");
        }
    }

    public void StopDustParticles()
    {
        StartCoroutine("StopParticle");
        
    }

    IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(1f);
        dustCloud.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collision.collider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool top = contactPoint.y > center.y;
    }
}
