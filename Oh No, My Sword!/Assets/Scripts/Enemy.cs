using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health = 3;
    public GameObject target;
    public float movementSpeed = 30f;

    public float decelerateSpeed = 0.95f;

    public float bounceSpeed = 0.5f;

	private Rigidbody2D rb2D;

    public int chargeIntervalSeconds = 5;

    private float timeSinceLastCharge;
    public bool isCharging = false;
    public bool hasCollided = false;

    public Vector3 lastDirection;

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log(tag);
        hasCollided = true;
        rb2D.AddRelativeForce(Vector2.Reflect(lastDirection.normalized, collision.contacts[0].normal) * bounceSpeed, ForceMode2D.Impulse);

         if (collision.gameObject.tag == "Breakable")
		{
			collision.gameObject.SetActive(false);
            UpdateHealth(-1);
            Debug.Log("Enemy health: " + health);
		}
    }

    void UpdateHealth(int value)
    {
        health += value;
    }

	void Start ()
    {
		rb2D = GetComponent<Rigidbody2D>();	
        timeSinceLastCharge = Time.time;
	}

    void FixedUpdate()
    {   
        float currentTime = Time.time;
        bool shouldCharge = !isCharging && (currentTime - timeSinceLastCharge > chargeIntervalSeconds);

        if (rb2D.velocity == Vector2.zero) {
            hasCollided = false;
            isCharging = false;
        }

        if (shouldCharge)
        {
            isCharging = true;
            ChargeAtTargetLastKnownPosition();
        }
        else if (hasCollided)
        {
            rb2D.velocity = rb2D.velocity * decelerateSpeed;
            rb2D.angularVelocity = rb2D.angularVelocity * decelerateSpeed; 
        }
    }

    void ChargeAtTargetLastKnownPosition()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = targetPosition - transform.position;
        lastDirection = direction;
        rb2D.AddRelativeForce(direction.normalized * movementSpeed, ForceMode2D.Force);
    }
}
