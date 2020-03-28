using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	public static Rigidbody2D rb;
	float dirX, moveSpeed = 5f;

    [SerializeField]
    GameObject dustCloud;

    bool coroutineAllowed, grounded;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(("just collided " + col.GetContact(0).point.ToString() + " "+ col.otherCollider.transform.position.ToString() + " " + col.otherCollider.bounds.ToString()));

        float delta = Mathf.Abs(col.GetContact(0).point.y - col.rigidbody.transform.position.y);

        if (col.gameObject.tag.Equals("Ground") && delta >= 0.1)
        {
            grounded = true;
            coroutineAllowed = true;
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("not colliding");
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
            coroutineAllowed = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		dirX = Input.GetAxisRaw ("Horizontal") * moveSpeed;

		if (Input.GetButtonDown ("Jump"))
			rb.AddForce (Vector2.up * 500f);

        if (grounded && rb.velocity.x != 0 && coroutineAllowed)
        {
            StartCoroutine("SpawnCloud");
            coroutineAllowed = false;
        }

        if (rb.velocity.x == 0 || !grounded)
        {
            StopCoroutine("SpawnCloud");
            coroutineAllowed = true;
        }
    }

	void FixedUpdate()
	{
		rb.velocity = new Vector2 (dirX, rb.velocity.y);
	}

    IEnumerator SpawnCloud()
    {
        while (grounded)
        {
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
    }

}
