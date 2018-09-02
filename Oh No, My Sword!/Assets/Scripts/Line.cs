using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
	public GameObject gameObject1;
	public GameObject gameObject2;
 
	private LineRenderer line;
 
	void Start () {
		line = this.gameObject.AddComponent<LineRenderer>();
		line.startWidth = 0.05F;
		line.endWidth = 0.5f;
		line.positionCount = 2;
	}
     
	void Update () {
		if (gameObject1 != null && gameObject2 != null)
		{
			line.SetPosition(0, gameObject1.transform.position);
			line.SetPosition(1, gameObject2.transform.position);
		}
	}
}
