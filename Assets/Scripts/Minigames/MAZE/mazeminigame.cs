//Darren Houn
//will attach to minigame prefab
//script to start the game and set controls and stats for the players in the game

using UnityEngine;
using System.Collections;
//using System;
using UnityEngine.UI;


public class mazeminigame : Minigame {

	//initialization of player and their stats
	private GameObject PlayerOne;
	private GameObject PlayerTwo;

	private Stats PlayerOneStats;
	private Stats PlayerTwoStats;

	private float Stopwatch;
	private bool StopwatchOn;
	public int SpeedP1 = 4;
	public int SpeedP2 = 4;
	public int Speed = 4;

	//outputs the entire float of the time
	public Text Stopwatchtxt;
		
	void Start ()
	{
		Debug.Log("Minigame Initializing!");
		//Initialize time
		Stopwatch = 0;

		PlayerOne = GameManager.Instance.Players[1].Key;
		PlayerTwo = GameManager.Instance.Players[2].Key;

		PlayerOneStats = GameManager.Instance.Players[1].Value;
		PlayerTwoStats = GameManager.Instance.Players[2].Value;

		//Set player's positions/controls
		//PlayerOne.transform.position = new Vector3(-30f, 5f, 0f);
		//PlayerTwo.transform.position = new Vector3(-20f, 5f, 0f);

		Debug.Log(PlayerOneStats.Intel);
		Debug.Log(PlayerOneStats.Str);
		Debug.Log(PlayerOneStats.Dex);

		//Sets the controls for w,a,s,d and i,j,k,l
		SetControls(PlayerOne);
		SetControls(PlayerTwo);
	}

	// Update logic for this minigame
	void Update()
	{
		if(!Finished)
		{
			CountUp ();
		}
	}

	//Counts the stopwatch up
	public int CountUp()
	{
		Stopwatch += Time.deltaTime * 1;
		Stopwatchtxt.text = "Time: " + Stopwatch.ToString ();
		return 0;
	}

	public override void UpHeldAction(GameObject player)
	{
		if (player == PlayerOne) {
			Speed = SpeedP1;
		} 
		else {
			Speed = SpeedP2;
		}
		Vector2 desPos = new Vector2 (0, Speed * Time.deltaTime);
		player.GetComponent<Rigidbody2D> ().MovePosition (player.GetComponent<Rigidbody2D>().position + desPos);
	}

	public override void LeftHeldAction(GameObject player)
	{
        Debug.Log("Tapped the Left key!");
        if (player == PlayerOne) {
			Speed = SpeedP1;
		} 
		else {
			Speed = SpeedP2;
		}
		Vector2 desPos = new Vector2 (-1 * Speed * Time.deltaTime, 0);
		player.GetComponent<Rigidbody2D> ().MovePosition (player.GetComponent<Rigidbody2D>().position + desPos);
	}

	public override void CenterHeldAction(GameObject player)
	{
		if (player == PlayerOne) {
			Speed = SpeedP1;
		} 
		else {
			Speed = SpeedP2;
		}
		Vector2 desPos = new Vector2 (0, -1 * Speed * Time.deltaTime);
		player.GetComponent<Rigidbody2D> ().MovePosition (player.GetComponent<Rigidbody2D>().position + desPos);
	}

	public override void RightHeldAction(GameObject player)
	{
        Debug.Log("Tapped the Right key!");
        if (player == PlayerOne) {
			Speed = SpeedP1;
		} 
		else {
			Speed = SpeedP2;
		}
		Vector2 desPos = new Vector2 (Speed * Time.deltaTime, 0);
		player.GetComponent<Rigidbody2D> ().MovePosition (player.GetComponent<Rigidbody2D>().position + desPos);
	}

	public override void UpTapAction(GameObject player)
	{
		Debug.Log("Tapped the up key!");
	}

	public override void LeftTapAction(GameObject player)
	{
		Debug.Log("Tapped the left key!");
	}

	public override void RightTapAction(GameObject player)
	{
		Debug.Log("Tapped the right key!");
	}

	public override void CenterTapAction(GameObject player)
	{
		Debug.Log("Tapped the center key!");
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

	public void GameEnd()
	{
		//Checks if the score of the first player is greater than the other player.
		//GameManager.Instance.QueueNewGame(); //Starts a new minigame. May modify to change the state of the game manager instead.
	}
}
