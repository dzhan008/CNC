/*
Created By: Rica Feng
Description: Driving script for Pongout minigame
Requirements: None
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PongOutMain : Minigame
{
    [SerializeField]
    private float Speed = 30f;

    private GameObject PlayerOne;
    private GameObject PlayerTwo;

    private Stats PlayerOneStats;
    private Stats PlayerTwoStats;

    //Used to display the timer, if needed.
    //public Text Timer;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Minigame Initializing!");
        //Initialize time
        TimerOn = false;
        TimeLeft = 5;

        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;
        //int s =  GameManager.Instance.Players[2].Value.Str = 0;
        //Set player's positions/controls
        //PlayerOne.transform.position = new Vector3(-30f, 5f, 0f);
        //PlayerTwo.transform.position = new Vector3(-20f, 5f, 0f);

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
                //Timer.text = "Time: " + (int)TimeLeft;
            }
        }
    }

    public override void UpTapAction(GameObject player)
    {
        //Debug.Log("Tapped the up key!");
    }

    public override void LeftTapAction(GameObject player)
    {
        //Debug.Log("Tapped the left key!");
    }

    public override void CenterTapAction(GameObject player)
    {
        //Debug.Log("Tapped the center key!");
    }

    public override void RightTapAction(GameObject player)
    {
        //Debug.Log("Tapped the right key!");
    }

    public override void UpHeldAction(GameObject player)
    {
        //player.transform.Translate(new Vector2(0, Speed) * Time.fixedDeltaTime);
        //if(player.GetComponent<PongOutPlayer>().holding != null )
        //{
        //    Debug.Log("Holding is true");
        //    player.GetComponent<PongOutPlayer>().holding.transform.Translate(new Vector2(0, Speed) * Time.deltaTime);
        //}
        //Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //rb.MovePosition(rb.position + new Vector2(0, Speed) * Time.deltaTime);
    }

    public override void LeftHeldAction(GameObject player)
    {
        player.transform.Translate(new Vector2(-Speed, 0) * Time.fixedDeltaTime);
        //Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //rb.MovePosition(rb.position + new Vector2(-Speed, 0) * Time.deltaTime);
    }

    public override void CenterHeldAction(GameObject player)
    {
        //player.transform.Translate(new Vector2(0, -Speed) * Time.fixedDeltaTime);
        //if (player.GetComponent<PongOutPlayer>().holding != null)
        //{
        //    Debug.Log("Holding is true");
        //    player.GetComponent<PongOutPlayer>().holding.transform.Translate(new Vector2(0, -Speed) * Time.deltaTime);
        //}
        //Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //rb.MovePosition(rb.position + new Vector2(0, -Speed) * Time.deltaTime);
    }

    public override void RightHeldAction(GameObject player)
    {
        player.transform.Translate(new Vector2(Speed, 0) * Time.fixedDeltaTime);
        //Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //rb.MovePosition(rb.position + new Vector2(Speed, 0) * Time.deltaTime);
    }

    public override void UpRelAction(GameObject player)
    {
        //Debug.Log("Released the up key!");
    }

    public override void LeftRelAction(GameObject player)
    {
        //Debug.Log("Released the left key!");
    }

    public override void CenterRelAction(GameObject player)
    {
        //Debug.Log("Released the center key!");
    }

    public override void RightRelAction(GameObject player)
    {
        //Debug.Log("Released the right key!");
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
