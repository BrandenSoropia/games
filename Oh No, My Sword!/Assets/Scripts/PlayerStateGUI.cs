using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGUI : MonoBehaviour {

	Player playerScript;
	public int initialPlayerHealth;
	public GameObject heartPrefab;
	public List<GameObject> instantiatedHearts = new List<GameObject>();

	public Vector3 paddingBetweenHearts = new Vector3(10, 0);
	public Vector3 rightMostHeartPosition; // Used for reference when adding/removing hearts

	void Start () {
		playerScript = FindObjectOfType<Player>();
		initialPlayerHealth = playerScript.health;

		UpdateHearts(initialPlayerHealth, false);
	}

	public void UpdateHearts(int numberOfHearts, bool isDealingDamage)
	{
		Vector3 containerPosition = gameObject.transform.position;
		Vector3 accumulatedPadding = Vector3.zero; // No padding on initial heart

		Vector3 newHeartPosition;
		GameObject heart;
		for(int i=0; i < numberOfHearts; i++)
		{	
			if (isDealingDamage)
			{
				int lastHeartIndex = instantiatedHearts.Count - 1;
				GameObject removedHeart = instantiatedHearts[lastHeartIndex];
				GameObject.Destroy(removedHeart);
				instantiatedHearts.RemoveAt(lastHeartIndex);

				rightMostHeartPosition = removedHeart.transform.position;
			}
			else
			{
				newHeartPosition = (
					rightMostHeartPosition == Vector3.zero
						? containerPosition
						: rightMostHeartPosition
				) + paddingBetweenHearts; // Handle udpating hearts if/if not empty

				rightMostHeartPosition = newHeartPosition;

				heart = Instantiate(heartPrefab, newHeartPosition, Quaternion.identity);
				instantiatedHearts.Add(heart);
			}
		}
	}
}
