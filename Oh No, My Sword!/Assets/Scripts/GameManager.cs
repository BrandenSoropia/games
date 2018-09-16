using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public Player playerScript; // Find all player scripts
	public Enemy enemyScript;
	public string gameOverSceneName;
	public bool isGameOver = false;
	public string gameOverMessage = "";

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start ()
	{
		playerScript = FindObjectOfType<Player>();
		enemyScript = FindObjectOfType<Enemy>();
	}
	
	void FixedUpdate ()
	{
		if (!isGameOver)
		{
			if (playerScript.health == 0)
			{
				isGameOver = true;
				gameOverMessage = "You lose!";
				StartCoroutine(LoadAsyncScene(gameOverSceneName));
			}

			if (enemyScript.health == 0)
			{
				isGameOver = true;
				gameOverMessage = "You win!";
				StartCoroutine(LoadAsyncScene(gameOverSceneName));
			}
		}
	}

	IEnumerator LoadAsyncScene(string sceneName)
	{
		// TODO: Pass over if win/loss to new scene to render appropriate message
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
	}
}
