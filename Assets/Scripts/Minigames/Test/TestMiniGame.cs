/*
Created By: David Zhang
Description: Sample script to handle start/update/end minigame logic in one script
Requirements: A minigame prefab.
*/

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TestMiniGame : Minigame {

    GameObject PlayerOne;
    GameObject PlayerTwo;

    Stats PlayerOneStats;
    Stats PlayerTwoStats;

    //Used to display the timer, if needed.
    public Text Timer;

    // Use this for initialization
    void Start ()
    {
        //Initialize time
        TimerOn = true;
        TimeLeft = 5;

        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;

        //Set player's positions/controls
        PlayerOne.transform.position = new Vector3(-30f, 5f, 0f);
        PlayerTwo.transform.position = new Vector3(-20f, 5f, 0f);

        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);
    }

	// Update logic for this minigame
    void Update()
    {
        if(!Finished && TimerOn)
        {
            if (CountDown(1) != 0)
            {
                Finished = true;
                GameEnd();
            }
            else
            {
                Timer.text = "Time: " + (int)TimeLeft;
            }
        }
    }

    public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }

    public override void LeftTapAction(GameObject player)
    {
        Debug.Log("Tapped the left key!");
    }

    public override void CenterTapAction(GameObject player)
    {
        Debug.Log("Tapped the center key!");
    }

    public override void RightTapAction(GameObject player)
    {
        Debug.Log("Tapped the right key!");
    }

    public override void UpHeldAction(GameObject player)
    {
        player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        player.transform.Translate(-0.5f, 0f, 0f);
    }

    public override void CenterHeldAction(GameObject player)
    {
        player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        player.transform.Translate(0.5f, 0f, 0f);
    }

    public override void UpRelAction(GameObject player)
    {
        Debug.Log("Released the up key!");
    }

    public override void LeftRelAction(GameObject player)
    {
        Debug.Log("Released the left key!");
    }

    public override void CenterRelAction(GameObject player)
    {
        Debug.Log("Released the center key!");
    }

    public override void RightRelAction(GameObject player)
    {
        Debug.Log("Released the right key!");
    }

    public override void GameEnd()
    {
        //Checks if the score of the first player is greater than the other player.
        if(PlayerOneStats.MiniGameScore > PlayerTwoStats.MiniGameScore)
        {
            Debug.Log("Player One wins!");
        }
        else if (PlayerOneStats.MiniGameScore < PlayerTwoStats.MiniGameScore)
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
