using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int health = 3;
	public float movementSpeed = 10f;
	public float decelerateSpeed = 0.9f;
	public float invulnerabilityAfterDamageTakenTimeSeconds = 1f;
	private float timeDamageTaken;
	public bool isInvulnerable = false;
	private Vector2 movement;

	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");

			movement.x = horizontal;
			movement.y = vertical;

			rb2D.AddRelativeForce(movement.normalized * movementSpeed, ForceMode2D.Force);
		}
		else
		{
			rb2D.velocity = rb2D.velocity * decelerateSpeed;
            rb2D.angularVelocity = rb2D.angularVelocity * decelerateSpeed; 
		}
	
		if (isInvulnerable && (Time.time - timeDamageTaken > invulnerabilityAfterDamageTakenTimeSeconds))
		{
			Debug.Log("Not Invulnerable!");
			isInvulnerable = false;
		}
	}

	void UpdateHealth(int value)
	{
		health += value;
		Debug.Log("Player Health: " + health);
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		float currentTime = Time.time;
        if (collision.gameObject.tag == "Breakable")
		{
			collision.gameObject.SetActive(false);
		}
		else if ((collision.gameObject.tag == "Enemy") && !isInvulnerable)
		{
			Debug.Log("Invulnerable!");
			UpdateHealth(-1);
			timeDamageTaken = currentTime;
			isInvulnerable = true;
		}
    }
}
