using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Player playerScript; // Find all player scripts
	public Enemy[] enemyScripts;

	void Start ()
	{
		playerScript = FindObjectOfType<Player>();
		enemyScripts = FindObjectsOfType<Enemy>();
	}
	
	void FixedUpdate ()
	{
		
		if (playerScript.health == 0)
		{
			Debug.Log("Game over!");
		}

		foreach (Enemy enemyScript in enemyScripts)
		{
			if (enemyScript.health == 0)
			{
				Debug.Log("You win!");
			}
		}
	}
}
