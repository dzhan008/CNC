/*
Created By: David Zhang
Description: Sample script to handle start/update/end minigame logic in one script
Requirements: A minigame prefab.
*/

using UnityEngine;
using System.Collections;
using System;

public class TestMiniGame : Minigame {

	// Use this for initialization
	void Start ()
    {
        //Initialize time
        TimeLeft = 5;
        //Set player's positions/controls
        GameObject PlayerOne = GameManager.Instance.PlayerOne;
        GameObject PlayerTwo = GameManager.Instance.PlayerTwo;

        PlayerOne.transform.position = new Vector3(-30f, 5f, 0f);
        PlayerTwo.transform.position = new Vector3(-20f, 5f, 0f);
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);
    }

	// Update logic for this minigame
    void Update()
    {
        if(!Finished)
        {
            if (CountDown(1) != 0)
            {
                Finished = true;
                GameEnd();
            }
        }
    }

    public override void LeftAction(GameObject player)
    {
        player.transform.Translate(-0.5f, 0f, 0f);
    }

    public override void CenterAction(GameObject player)
    {
        player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void RightAction(GameObject player)
    {
        player.transform.Translate(0.5f, 0f, 0f);
    }

    public override void GameEnd()
    {
        //Checks if the score of the first player is greater than the other player.
        if(GameManager.Instance.PlayerOne.GetComponent<Stats>().MiniGameScore > GameManager.Instance.PlayerTwo.GetComponent<Stats>().MiniGameScore)
        {
            Debug.Log("Player One wins!");
        }
        else if (GameManager.Instance.PlayerOne.GetComponent<Stats>().MiniGameScore < GameManager.Instance.PlayerTwo.GetComponent<Stats>().MiniGameScore)
        {
            Debug.Log("Player Two wins!");
        }
        else
        {
            Debug.Log("It is a tie!");
        }
        GameManager.Instance.QueueNewGame(); //Starts a new minigame. May modify to change the state of the game manager instead.
    }
}
