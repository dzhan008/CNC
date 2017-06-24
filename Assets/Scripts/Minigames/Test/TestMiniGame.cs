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

    private GameObject PlayerOne;
    private GameObject PlayerTwo;

    private Stats PlayerOneStats;
    private Stats PlayerTwoStats;

    //Used to display the timer, if needed.
    public Text Timer;



    // Use this for initialization
    void Start ()
    {
        Debug.Log("Minigame Initializing!");
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

    public override void OnStart()
    {
        
    }

    public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }

    public override void LeftTapAction(GameObject player)
    {
        Vector2 side = player.transform.localScale;
        side.x = (player.transform.localScale.x < 0f) ? -1 * player.transform.localScale.x : player.transform.localScale.x;
        player.transform.localScale = side;
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
    }

    public override void CenterTapAction(GameObject player)
    {
        Debug.Log("Tapped the center key!");
    }

    public override void RightTapAction(GameObject player)
    {
        Vector2 side = player.transform.localScale;
        side.x = (player.transform.localScale.x > 0f) ? -1 * player.transform.localScale.x : player.transform.localScale.x;
        player.transform.localScale = side;
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
    }

    public override void UpHeldAction(GameObject player)
    {
        player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        player.transform.Translate(-0.5f, 0f, 0f);
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
    }

    public override void CenterHeldAction(GameObject player)
    {
        player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        player.transform.Translate(0.5f, 0f, 0f);
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
    }

    public override void UpRelAction(GameObject player)
    {
        Debug.Log("Released the up key!");
    }

    public override void LeftRelAction(GameObject player)
    {
        player.GetComponentInChildren<Animator>().SetFloat("Running", 0);
    }

    public override void CenterRelAction(GameObject player)
    {
        Debug.Log("Released the center key!");
    }

    public override void RightRelAction(GameObject player)
    {
        player.GetComponentInChildren<Animator>().SetFloat("Running", 0);
    }
}
