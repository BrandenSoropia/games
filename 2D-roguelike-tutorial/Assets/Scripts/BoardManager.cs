using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);

    // Holds prefabs for our game
    public GameObject exit;
    // Keep references to variations of same type of GO that can be used on board
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder; // Parent containing all board GO hierarchy, can collapse in inspector
    private List<Vector3> gridPositions = new List<Vector3>(); // Keep track of all pos in board, i.e GO in each pos

	void InitializeList()
	{
        gridPositions.Clear();

        // Fill list of usable pos in board as Vector 3, from left to right and bot to top
        // Used to create list of posible pos to place walls, enemies and pickups
        // NOTE: somehow manages to keep the outermost perimeter of the board open, keeping that path open always
        for (int x = 1; x < columns - 1; x++) 
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));    
            }
        }
	}

    void BoardSetup() // Setup and instantiate outwerwall and floor
    {
        boardHolder = new GameObject("Board").transform;

        // Uses -1 to build and edge around the outer board, outside of the playable area
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)]; // Choose a random floor tile
                // Check if outwall pos, use outer wall
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                // Quaternion.identity == no rotation, "as GameObject" == casting
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    // Used to spawn GO at random position in playable section of board.
    // Position removed in list to ensure no dupes
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);

        return randomPosition; 
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1); // # objects to spawn of tile array

        for (int i = 0; i < objectCount; i++) // Instantiate GO on position
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    // NOTE: Only public function in class, it is the only function to be called by GameManager to setup board 
    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        // Create logarithmic difficulty curve for enemies based on level
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        // Exit always in top right most corner of board. Since based off col/rows, it will always be there
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}
