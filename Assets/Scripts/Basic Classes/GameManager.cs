using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    public GameObject playerOne;
    public GameObject playerTwo;

    List<GameObject> miniGames; //List of prefabs to be loaded
    GameObject currentMiniGame;

	//Add all prefabs here and queue all the minigames randomly.
	void Start ()
    {
        miniGames = new List<GameObject>();
        miniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));
        miniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));

        GameObject newGame = (GameObject)Instantiate(miniGames[0]);
        currentMiniGame = newGame;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Destroys the old minigame, and instantiates a new minigame from the list.
    public void QueueNewGame()
    {
        playerOne.GetComponent<Stats>().resetMiniGameScore();
        playerTwo.GetComponent<Stats>().resetMiniGameScore();

        GameObject minigame = currentMiniGame;
        miniGames.RemoveAt(0);
        Destroy(minigame);
        currentMiniGame = (GameObject)Instantiate(miniGames[0]);
    }
}
