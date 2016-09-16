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
    public Dictionary<int, KeyValuePair<GameObject, Stats>> Players;
    private List<int> PlayerIDs;

    private List<GameObject> MiniGames; //List of prefabs to be loaded
    private GameObject CurrentMiniGame;

    void Awake()
    {
        Players = new Dictionary<int, KeyValuePair<GameObject, Stats> >();
        PlayerIDs = new List<int>();
        GameObject[] tempPlayers = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < tempPlayers.Length; i++)
        {
            Players.Add(tempPlayers[i].GetComponent<Stats>().Id, new KeyValuePair<GameObject, Stats>(tempPlayers[i], tempPlayers[i].GetComponent<Stats>()));
            PlayerIDs.Add(tempPlayers[i].GetComponent<Stats>().Id);
        }
    }

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
        for(int i = 0; i < PlayerIDs.Count; i++)
        {
                Players[PlayerIDs[i]].Value.ResetMiniGameScore();
        }
        GameObject mini_game = CurrentMiniGame;
        MiniGames.RemoveAt(0);
        Destroy(mini_game);
        CurrentMiniGame = (GameObject)Instantiate(MiniGames[0]);
    }
}
