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

    [SerializeField]
    BarScript PlayerOneObstacleBar;
    [SerializeField]
    BarScript PlayerTwoObstacleBar;


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
	{
		float speed = 0;
        float slow = 0;
        speed = player.GetComponent<PlayerStat>().PSkills["baseSpeed"] + player.GetComponent<PlayerStat>().PSkills["sprintSpeedAdd"];

        slow = playerDrag + player.GetComponent<PlayerStat>().PSkills["playerSlowAdd"];
        float totalSpeed = speed - slow;
		player.transform.Translate(totalSpeed, 0f, 0f);
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
        PlayerTwo.transform.position = new Vector3(-30f, 3.94f, 0f);

        PlayerOne.AddComponent<PlayerStat>();
        PlayerTwo.AddComponent<PlayerStat>();


        PlayerOne.GetComponent<PlayerStat>().Initialize(PlayerOneStats, PlayerOneSprintBar, PlayerOneObstacleBar);
        PlayerTwo.GetComponent<PlayerStat>().Initialize(PlayerTwoStats, PlayerTwoSprintBar, PlayerTwoObstacleBar);

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
        float offset = 0.8f;
        //find the new x position of the camera to be in the middle of two players
        float posX = Mathf.SmoothDamp(MainCamera.transform.position.x,
            MidPointFormula() + offset, ref CameraVelocity.x, SmoothTimeX);
        //Change the camera's position
        MainCamera.transform.position = new Vector3(posX, 5f,
            MainCamera.transform.position.z);
        
    }
    void updateSprint(GameObject player)
    {
        float playerIsSprint = player.GetComponent<PlayerStat>().PSkills["isSprint"];
        float playerSprintStart = player.GetComponent<PlayerStat>().PSkills["sprintStartTime"];
        float playerSprintDuration = player.GetComponent<PlayerStat>().PSkills["sprintDuration"];
        if (playerIsSprint == 1) //I'M SORRY I MUST DO THIS
        {
            //reset if duration is done
            if ((playerSprintStart += Time.deltaTime) > playerSprintDuration)
            {
                player.GetComponent<PlayerStat>().PSkills["isSprint"] = 0;
                player.GetComponent<PlayerStat>().PSkills["sprintStartTime"] = 0;
                player.GetComponent<PlayerStat>().PSkills["sprintSpeedAdd"] = 0;
            }
            //increase distance and update
            else
            {
                player.GetComponent<PlayerStat>().PSkills["sprintSpeedAdd"] = 0.1f;  
                //player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + (player.transform.right * 15f * Time.deltaTime));
                player.GetComponent<PlayerStat>().PSkills["sprintStartTime"] = playerSprintStart;
            }
        }
        //if the player isn't sprinting and their sprint bar isn't full
        else if (player.GetComponent<PlayerStat>().SprintCurrentVal != 50 && playerIsSprint != 1)
        {
            player.GetComponent<PlayerStat>().SprintCurrentVal += (2 * Time.deltaTime);
        }
    }

    void updateCockBlock(GameObject player)
    {
        if (player.GetComponent<PlayerStat>().ObstacleCurrentVal != 3)
        {
            float rechargeSpeed = player.GetComponent<PlayerStat>().PSkills["chickenChargeRate"];
            player.GetComponent<PlayerStat>().ObstacleCurrentVal += (rechargeSpeed * Time.deltaTime);
        }
    }

    void updateObstacleSpawn(GameObject player)
    {
        //if the obstacle bar isn't full 
        if (player.GetComponent<PlayerStat>().ObstacleCurrentVal != 3)
        {
            player.GetComponent<PlayerStat>().ObstacleCurrentVal += (2 * Time.deltaTime);
        }
    }
    // Update logic for this minigame
    void Update()
    {
        if (false) GameEnd();
		updateSpeed(PlayerOne);
		updateSpeed(PlayerTwo);
        updateSprint(PlayerOne);
        updateSprint(PlayerTwo);
        updateCockBlock(PlayerOne);
        updateCockBlock(PlayerTwo);
        updateCamera();
    }

 	public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }
		
    public override void LeftTapAction(GameObject player)
    {
		float jump_height = 0;
        jump_height = player.GetComponent<PlayerStat>().PSkills["jumpHeight"];
        Debug.Log(jump_height);
		if (player.GetComponent<PlayerCollision>().CanJump) {
			player.GetComponent<Rigidbody2D> ().AddForce (player.transform.up * jump_height);
		}
    }

    public override void CenterTapAction(GameObject player)
    {
        if (player.GetComponent<PlayerStat>().PSkills["isSprint"] == 0 && 
            player.GetComponent<PlayerStat>().SprintCurrentVal == 50)
        {
            player.GetComponent<PlayerStat>().SprintCurrentVal -= 50;
            player.GetComponent<PlayerStat>().PSkills["isSprint"] = 1;
            player.GetComponent<PlayerStat>().PSkills["sprintStartTime"] = 0;
        }
    }

    public override void RightTapAction(GameObject player)
    {
        Debug.Log("Tapped the right key!");
        //spawn obstacle
        //Debug.Log(player.GetComponent<PlayerStat>().ObstacleCurrentVal);
        if (player.GetComponent<PlayerStat>().ObstacleCurrentVal >= 1)
        {
            player.GetComponent<PlayerStat>().ObstacleCurrentVal -= 1;
            player.GetComponent<PlayerStat>().spawnCockBlockObstacle();


        }
    }

    public override void UpHeldAction(GameObject player)
    {
    }

    public override void LeftHeldAction(GameObject player)
    {
        //player.transform.Translate(-0.5f, 0f, 0f);

    }

    public override void CenterHeldAction(GameObject player)
    {
        
    }

    public override void RightHeldAction(GameObject player)
    {
        //player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + (player.transform.right * 8f * Time.deltaTime));
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

