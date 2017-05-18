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
    public States GameState;

    public Dictionary<int, KeyValuePair<GameObject, Stats>> Players;
    private List<int> PlayerIDs;

    private List<GameObject> MiniGames; //List of prefabs to be loaded
    private GameObject CurrentMiniGame;
    private int CurrentMiniGameIndex = 0;

    void Awake()
    {

        Players = new Dictionary<int, KeyValuePair<GameObject, Stats>>();
        PlayerIDs = new List<int>();
        GameObject[] tempPlayers = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < tempPlayers.Length; i++)
        {
            Players.Add(tempPlayers[i].GetComponent<Stats>().Id, new KeyValuePair<GameObject, Stats>(tempPlayers[i], tempPlayers[i].GetComponent<Stats>()));
            PlayerIDs.Add(tempPlayers[i].GetComponent<Stats>().Id);
        }
        Debug.Log("Game Manager Initializing!");
    }

    /// <summary>
    /// Initializes the GameManager, loading in all the minigames randomly and starting the first minigame.
    /// </summary>
    void Start()
    {
        MiniGames = new List<GameObject>();
        MiniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Swift Smiths/Swift Smiths"));
        MiniGames.Add((GameObject)Resources.Load("Prefabs/Minigames/Test"));
        //GameState = States.Debug;
        if (GameState != States.Debug)
        {

            //LoadMiniGame();
        }

    }

    public void LoadMiniGame(string name)
    {
        MiniGames.Add(ResourceManager.Instance.MiniGames[name]);
        CurrentMiniGameIndex = MiniGames.Count - 1;
        GameObject new_game = (GameObject)Instantiate(MiniGames[CurrentMiniGameIndex]);
        CurrentMiniGame = new_game;
    }

    public void LoadMiniGame()
    {
        CurrentMiniGameIndex = Random.Range(0, MiniGames.Count);
        GameObject new_game = (GameObject)Instantiate(MiniGames[CurrentMiniGameIndex]);
        CurrentMiniGame = new_game;
        GameState = States.InGame; //NOTE: We might need to move this into the minigames because we need to lock the controls somehow avoiding errors
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayProgress()
    {
        Debug.Log("DISPLAYINGPROGRESS");
        /*Change after demo */
        Destroy(CurrentMiniGame);
        UIManager.Instance.ShowMiniGameScreen();
        //StartCoroutine(CoroutineProgress(2));
        GameState = States.Results;
    }

    /// <summary>
    /// Destroys the old minigame, and instantiates a new minigame from the list.
    /// </summary>
    public void QueueNewGame()
    {
        StartCoroutine(CoroutineQueueGame(1));
    }

    /// <summary>
    /// Destroys the current minigame.
    /// </summary>
    public void DestroyMiniGame()
    {
        GameObject mini_game = CurrentMiniGame;
        MiniGames.RemoveAt(CurrentMiniGameIndex);
        Destroy(mini_game);
    }

    IEnumerator CoroutineProgress(float time)
    {
        UIManager.Instance.FadeIn();
        yield return new WaitForSeconds(time);
        DestroyMiniGame();
        AudioManager.Instance.StopSounds();
        UIManager.Instance.FadeOut();
        UIManager.Instance.ShowProgressScreen();
    }

    IEnumerator CoroutineQueueGame(float time)
    {
        UIManager.Instance.FadeIn();
        yield return new WaitForSeconds(time);
        UIManager.Instance.DisableProgressScreen();
        for (int i = 0; i < PlayerIDs.Count; i++)
        {
            Players[PlayerIDs[i]].Value.ResetMiniGameScore();
        }
        LoadMiniGame();
        UIManager.Instance.FadeOut();
    }
}
