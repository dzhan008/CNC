/*
Created By: David Zhang
Description: Handles the flow of the game itself and plays all the minigames.
Requirements: None.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    List<GameObject> MiniGames; //List of prefabs to be loaded
    GameObject CurrentMiniGame;

    /// <summary>
    /// Initializes the GameManager, loading in all the minigames randomly and starting the first minigame.
    /// </summary>
    void Start ()
    {
        MiniGames = new List<GameObject>();
        MiniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));
        MiniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));

        GameObject new_game = (GameObject)Instantiate(MiniGames[0]);
        CurrentMiniGame = new_game;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /// <summary>
    /// Destroys the old minigame, and instantiates a new minigame from the list.
    /// </summary>
    public void QueueNewGame()
    {
        PlayerOne.GetComponent<Stats>().ResetMiniGameScore();
        PlayerTwo.GetComponent<Stats>().ResetMiniGameScore();

        GameObject mini_game = CurrentMiniGame;
        MiniGames.RemoveAt(0);
        Destroy(mini_game);
        CurrentMiniGame = (GameObject)Instantiate(MiniGames[0]);
    }
}
