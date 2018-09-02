using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	private GameObject gameObject;

	void Start()
	{
		gameObject = GetComponent<GameObject>();
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
