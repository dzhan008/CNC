using UnityEngine;
using System.Collections;
using System;

public class TestMiniGame : Minigame {

	// Use this for initialization
	void Start ()
    {
        //Initialize time
        timeLeft = 5;
        //Set player's positions/controls
        GameObject playerOne = GameManager.Instance.playerOne;
        GameObject playerTwo = GameManager.Instance.playerTwo;
        playerOne.transform.position = new Vector3(-30f, 5f, 0f);
        playerTwo.transform.position = new Vector3(-20f, 5f, 0f);
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        setControls(playerOne);
        setControls(playerTwo);
    }

	// Update logic minigame
    void Update()
    {
        if(!finished)
        {
            if (countDown() != 0)
            {
                finished = true;
                GameEnd();
            }
        }

    }

    public override void leftAction(GameObject player)
    {
        player.transform.Translate(-0.5f, 0f, 0f);
    }

    public override void centerAction(GameObject player)
    {
        player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void rightAction(GameObject player)
    {
        player.transform.Translate(0.5f, 0f, 0f);
    }

    public override void GameEnd()
    {
        if(GameManager.Instance.playerOne.GetComponent<Stats>().miniGameScore > GameManager.Instance.playerTwo.GetComponent<Stats>().miniGameScore)
        {
            Debug.Log("Player One wins!");
        }
        else
        {
            Debug.Log("Player Two wins!");
        }
        GameManager.Instance.QueueNewGame();
    }
}
