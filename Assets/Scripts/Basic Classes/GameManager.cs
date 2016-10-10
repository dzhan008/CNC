/*
Created By: David Zhang
Description: Handles the flow of the game itself and plays all the minigames.
Requirements: None.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum States
{
    MainMenu = 0,
    InGame,
    Results,
    EndGame,
    Debug
}

public class GameManager : Singleton<GameManager>
{
    States GameStates;

    public Dictionary<int, KeyValuePair<GameObject, Stats>> Players;
    private List<int> PlayerIDs;

    private List<GameObject> MiniGames; //List of prefabs to be loaded
    private GameObject CurrentMiniGame;
    private int CurrentMiniGameIndex = 0;

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
        Debug.Log("Game Manager Initializing!");
    }

    /// <summary>
    /// Initializes the GameManager, loading in all the minigames randomly and starting the first minigame.
    /// </summary>
    void Start ()
    {
        GameStates = States.Debug;
        if(GameStates != States.Debug)
        {
            MiniGames = new List<GameObject>();
            MiniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));
            MiniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));
            LoadMiniGame();
        }

	}

    private void LoadMiniGame()
    {
        CurrentMiniGameIndex = Random.Range(0, MiniGames.Count);
        GameObject new_game = (GameObject)Instantiate(MiniGames[CurrentMiniGameIndex]);
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
        MiniGames.RemoveAt(CurrentMiniGameIndex);
        Destroy(mini_game);
        LoadMiniGame();
    }
}
