using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DragonMiniGame : Minigame
{
    GameObject PlayerOne;
    GameObject PlayerTwo;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    Camera MainCamera;
    public float SmoothTimeX;
    public float SmoothTimeY;
    public Vector2 CameraVelocity;
    Stats PlayerOneStats;
    Stats PlayerTwoStats;

    [SerializeField]
    BarScript PlayerOneSprintBar;
    [SerializeField]
    BarScript PlayerTwoSprintBar;

    PlayerStat P1Stat;
    PlayerStat P2Stat;
    //Used to display the timer, if needed.
    public Text Timer;

	public float playerDrag = 5;
    /*Contains the skills/abilities value of each player*/

    private void Awake()
    {
		//PlayerOne.GetComponent<PlayerStat>().Initialize(PlayerOneStats);
		//PlayerTwo.GetComponent<PlayerStat>().Initialize(PlayerTwoStats);
    }
    
	void updateSpeed(GameObject player)
	{/*
		float speed = 0;
		if (player == PlayerOne)
			speed = P1Skills ["baseSpeed"];
		else
			speed = P2Skills ["baseSpeed"];
		float totalSpeed = speed - playerDrag;
		player.transform.Translate(totalSpeed, 0f, 0f);*/
	}

    void Start()
    {
        //Initialize time
        TimerOn = false;
        TimeLeft = 5000;
        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;
        //Initialize time
        TimeLeft = 5;
        //Set player's positions/controls
        PlayerOne.transform.position = new Vector3(-30f, 7.48f, 0f);
        PlayerTwo.transform.position = new Vector3(-30f, 2.81f, 0f);

        PlayerOne.AddComponent<PlayerStat>();
        PlayerTwo.AddComponent<PlayerStat>();


        PlayerOne.GetComponent<PlayerStat>().Initialize(PlayerOneStats, PlayerOneSprintBar);
        PlayerTwo.GetComponent<PlayerStat>().Initialize(PlayerTwoStats, PlayerTwoSprintBar);

        //P2Stat.Initialize(PlayerTwoStats);
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);

        //Init the offset of camera
        offset = MainCamera.transform.position - PlayerOne.transform.position;
    }

    float MidPointFormula()
    {
        return (PlayerOne.transform.position.x + PlayerTwo.transform.position.x) / 2;
    }
    void updateCamera()
    {
        //find the new x position of the camera to be in the middle of two players
        float posX = Mathf.SmoothDamp(MainCamera.transform.position.x,
            MidPointFormula(), ref CameraVelocity.x, SmoothTimeX);
        //Change the camera's position
        MainCamera.transform.position = new Vector3(posX, 5f,
            MainCamera.transform.position.z);
    }
    // Update logic for this minigame
    void Update()
    {
        //MainCamera.transform.Translate(0.1f, 0f, 0f);
        if (!Finished && TimerOn)
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
		updateSpeed(PlayerOne);
		updateSpeed(PlayerTwo);
        updateCamera();
    }

 	public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }
		
    public override void LeftTapAction(GameObject player)
    {
		float jump_height = 0;
        jump_height = player.GetComponent<PlayerStat>().returnDictionary("jumpHeight");
        Debug.Log(jump_height);
		if (player.GetComponent<PlayerCollision>().CanJump) {
			player.GetComponent<Rigidbody2D> ().AddForce (player.transform.up * jump_height);
		}
    }


	public void updateSprint (GameObject player)
	{
		
	}
    public override void CenterTapAction(GameObject player)
    {
        player.GetComponent<PlayerStat>().SprintCurrentVal -= 10;
	
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
        //player.transform.Translate(-0.5f, 0f, 0f);

    }

    public override void CenterHeldAction(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + (player.transform.right * 15f * Time.deltaTime));
    }

    public override void RightHeldAction(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + (player.transform.right * 8f * Time.deltaTime));
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
        if (PlayerOneStats.MiniGameScore > PlayerTwoStats.MiniGameScore)
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

