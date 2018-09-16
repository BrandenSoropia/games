using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class GameOverGUI : MonoBehaviour {
	void Start()
	{
		GameManager gameManager = FindObjectOfType<GameManager>();
		Text textGameObject = GetComponentInChildren<Text>();

		textGameObject.text = gameManager.gameOverMessage;
	}
}
