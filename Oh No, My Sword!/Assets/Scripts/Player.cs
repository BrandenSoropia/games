using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float movementSpeed = 10f;

	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		rb2D.MovePosition(new Vector2(rb2D.position.x + horizontal, rb2D.position.y + vertical));
	}

		void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Breakable")
		{
			Debug.Log("Removing" + collision.gameObject.name);
			collision.gameObject.SetActive(false);
		}
    }
}
