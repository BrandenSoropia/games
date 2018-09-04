using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGUI : MonoBehaviour {

	Player playerScript;
	public int initialPlayerHealth;
	public GameObject heartPrefab;
	public List<GameObject> instantiatedHearts = new List<GameObject>();

	public Vector3 paddingBetweenHearts = new Vector3(10, 0);
	public Vector3 lastHeartPosition; // Used for reference when adding/removing hearts

	void Start () {
		playerScript = FindObjectOfType<Player>();
		initialPlayerHealth = playerScript.health;

		UpdateHearts(initialPlayerHealth, false);
	}

	void UpdateHearts(int numberOfHearts, bool isRemovingHearts)
	{
		Vector3 containerPosition = gameObject.transform.position;
		Vector3 accumulatedPadding = Vector3.zero; // No padding on initial heart

		Vector3 newHeartPosition;
		GameObject heart;
		for(int i=0; i < numberOfHearts; i++)
		{	
			if (isRemovingHearts)
			{
				int lastHeartIndex = instantiatedHearts.Count - 1;
				GameObject removedHeart = instantiatedHearts[lastHeartIndex];
				GameObject.Destroy(removedHeart);
				instantiatedHearts.RemoveAt(lastHeartIndex);

				lastHeartPosition -= accumulatedPadding;
			}
			else
			{
				newHeartPosition = (
					lastHeartPosition == Vector3.zero
						? containerPosition
						: lastHeartPosition
				) + accumulatedPadding; // Handle udpating hearts if/if not empty

				lastHeartPosition = newHeartPosition;

				heart = Instantiate(heartPrefab, newHeartPosition, Quaternion.identity);
				instantiatedHearts.Add(heart);
			}
			
			accumulatedPadding += paddingBetweenHearts;
		}
	}
	
	void FixedUpdate () {
		int healthDifference = Mathf.Abs(playerScript.health - initialPlayerHealth);
		bool isRemovingHearts = playerScript.health < initialPlayerHealth;
		Debug.Log(healthDifference);
		if (healthDifference != 0)
		{
			UpdateHearts(healthDifference, isRemovingHearts);
		}
	}
}
