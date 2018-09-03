using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Player[] players; // Find all player scripts
	public Enemy[] enemies;

	void Start ()
	{
		players = FindObjectsOfType<Player>();
		enemies = FindObjectsOfType<Enemy>();
	}
	
	void FixedUpdate ()
	{
		foreach (Player player in players)
		{
			if (player.health == 0)
			{
				Debug.Log("Game over!");
			}
		}

		foreach (Enemy enemy in enemies)
		{
			if (enemy.health == 0)
			{
				Debug.Log("Game over!");
			}
		}
	}
}
