using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableManager : MonoBehaviour {
	public GameObject breakable;
	public GameObject container;
	public int numberOfBreakablesToGenerate = 1;
	public Vector3 containerPosition;

	public Vector3 containerExtents;
	public Vector3 breakableSize;
	public Vector3 containableSpace;

	

	// Use this for initialization
	void Start () {
		containerPosition = container.transform.position;
		containerExtents = container.GetComponent<SpriteRenderer>().bounds.extents;
		breakableSize = breakable.GetComponent<SpriteRenderer>().bounds.size;

		// NOTE: Assumes container position is at origin of world: Vector(0, 0 ,0);
		// Calculate space that can fit gameObject to spawn
		containableSpace = new Vector3(containerExtents.x - breakableSize.x, containerExtents.y - breakableSize.y, 0);


		SpawnInRandomPointWithinContainer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnInRandomPointWithinContainer() {
		for (int i=0; i < numberOfBreakablesToGenerate; i++) {
			Vector3 position = new Vector3(
				Random.Range(-containableSpace.x, containableSpace.x),
				Random.Range(-containableSpace.y, containableSpace.y),
				0
			);

			// TODO: Do not spawn if player, enemy or other breakable exists with that area.

			GameObject newBreakable = Instantiate(breakable, this.transform, false);
			newBreakable.transform.localPosition = position;
		}
	}
}
