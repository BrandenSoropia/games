using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
	public int damage = 1;

	void OnCollisionEnter2D(Collision2D collision)
    {
		if ((collision.gameObject.tag == "Enemy") || collision.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
		}
    }
}
