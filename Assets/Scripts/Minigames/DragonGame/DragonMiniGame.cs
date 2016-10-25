using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DragonMiniGame : Minigame
{
    GameObject PlayerOne;
    GameObject PlayerTwo;

    Stats PlayerOneStats;
    Stats PlayerTwoStats;
    //Used to display the timer, if needed.
    public Text Timer;

	[SerializeField]
	private GameObject GroundOne;
	[SerializeField]
	private GameObject GroundTwo;

	public float playerDrag = 5;
    /*Contains the skills/abilities value of each player*/
    Dictionary<string, float> P1Skills = new Dictionary<string, float>();
    Dictionary<string, float> P2Skills = new Dictionary<string, float>();

    /// <summary>
    /// Description: Calculates the duration of sprint
    /// </summary>
    float sprintDurationCalc(Stats Player)
    {
        return 1;
    }

    /// <summary>
    /// Description: Calculates the cooldown of chicken charges
    /// </summary>
    float chickenChargeRateCalc(Stats Player)
    {
        return 1;
    }

    /// <summary>
    /// Description: Calculates the bonus jump height
    /// </summary>
    float jumpHeightCalc(Stats Player)
    {
        return 400;
    }
	float baseSpeedCalc(Stats Player)
	{
		return 5;
	}
	void updateSpeed(GameObject player)
	{
		float speed = 0;
		if (player == PlayerOne)
			speed = P1Skills ["baseSpeed"];
		else
			speed = P2Skills ["baseSpeed"];
		float totalSpeed = speed - playerDrag;
		player.transform.Translate(totalSpeed, 0f, 0f);
	}
    // Use this for initialization
    void InitSkills()
    {
        //Strength P1
        P1Skills.Add("sprintbar", 100);
        P1Skills.Add("sprintChargeRate", 1);
        P1Skills.Add("sprintDuration", sprintDurationCalc(PlayerOneStats));
        //Intelligence P1
        P1Skills.Add("chickenBar", 100);
        P1Skills.Add("chickenChargeRate", chickenChargeRateCalc(PlayerOneStats));
        P1Skills.Add("chickenCharges", 3);
        //Dexterity P1
        P1Skills.Add("jumpHeight", jumpHeightCalc(PlayerOneStats));
		P1Skills.Add("baseSpeed", baseSpeedCalc(PlayerOneStats));

        //Strength P2
        P2Skills.Add("sprintbar", 100);
        P2Skills.Add("sprintChargeRate", 1);
        P2Skills.Add("sprintDuration", sprintDurationCalc(PlayerTwoStats));
        //Intelligence P2
        P2Skills.Add("chickenBar", 100);
        P2Skills.Add("chickenChargeRate", chickenChargeRateCalc(PlayerTwoStats));
        P2Skills.Add("chickenCharges", 3);
        //Dexterity P2
        P2Skills.Add("jumpHeight", jumpHeightCalc(PlayerTwoStats));
		P2Skills.Add("baseSpeed", baseSpeedCalc(PlayerTwoStats));

    }
    void Start()
    {
        //Set the skill values of each player
        InitSkills();
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

        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);
    }

    // Update logic for this minigame
    void Update()
    {
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
    }

 	public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }
		
    public override void LeftTapAction(GameObject player)
    {
		float jump_height = 0;
		GameObject ground; 
		if (player == PlayerOne) {
			jump_height = P1Skills["jumpHeight"];
			ground = GroundOne;

		} else {
			jump_height = P2Skills["jumpHeight"];
			ground = GroundTwo;
		}
		
		if (ground.GetComponent<Ground>().isColliding) {
			
			player.GetComponent<Rigidbody2D> ().AddForce (player.transform.up * jump_height);
		}


    }


	public void updateSprint (GameObject player)
	{
		
	}
    public override void CenterTapAction(GameObject player)
    {
		float sprint_duration = 0;
		if (player == PlayerOne) {
			sprint_duration = P1Skills["sprintDuration"];

		} else {
			sprint_duration = P2Skills["sprintDuration"];
		}
			
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

