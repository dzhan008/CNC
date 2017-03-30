/*
Created By: David Zhang
Description: Sample script to handle start/update/end minigame logic in one script
Requirements: A minigame prefab.
*/

//2.413333

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class StackOverflowedMinigame : Minigame {

    //player objects created here
    private GameObject PlayerOne;
    private GameObject PlayerTwo;

    private Stats PlayerOneStats;
    private Stats PlayerTwoStats;

    //--------Scoring-----------//
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text finishedText;

    //Used to display the timer, if needed.
    public Text Timer;
    int displayTime = 0;
    private ObjectPooler bookSpawner;

    //for spawning books
    [SerializeField]
    private List<GameObject> spawnPoints;

    private float timePassed = 0f;
    private float spawnRate = 0f;
    private float addTime = 4f;

    //spawn points for initial character spawn
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Minigame Initializing!");
        //Initialize time and set score to 0
        TimerOn = true;
        TimeLeft = 3;

        //disbale finished text until game is won
        finishedText.enabled = false;

        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;

        PlayerOneStats.MiniGameScore = 0;
        PlayerTwoStats.MiniGameScore = 0;

        PlayerOne.transform.position = SpawnPoint1.transform.position;
        PlayerTwo.transform.position = SpawnPoint2.transform.position;

        //Set player's positions/controls
        //PlayerOne.transform.position = new Vector3(-30f, 5f, 0f);
        //PlayerTwo.transform.position = new Vector3(-20f, 5f, 0f);

        //object pooling stuff
        bookSpawner = this.GetComponent<ObjectPooler>();
        spawnRate = Time.time + addTime;
        InvokeRepeating("spawnBook", 1.0f, 0.5f);

        Debug.Log(PlayerOneStats.Intel);
        Debug.Log(PlayerOneStats.Str);
        Debug.Log(PlayerOneStats.Dex);

        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);

   
    }

    //Game is finished complete the game now :D
    void Game_Finished()
    {
        //disable player rigid body to stop all movmenet
        finishedText.enabled = true;

        //stop all the invokes spawning books
        CancelInvoke();

        PlayerOne.tag = "Book";
        PlayerTwo.tag = "Book";

        //get rid of all the books on the players
        for (int i = 0; i < PlayerOne.transform.childCount; ++i)
        {

            Debug.Log("Books: " + i + " " + PlayerOne.transform.GetChild(i).gameObject.tag);

            PlayerOne.transform.GetChild(i).gameObject.SetActive(false);
            //reset tags and information
            PlayerOne.transform.GetChild(i).gameObject.tag = "Book";
            PlayerOne.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("BookLayer");
            PlayerOne.transform.GetChild(i).gameObject.GetComponent<BookStackingScript>().touched = false;
            PlayerOne.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            PlayerOne.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }

        while (PlayerOne.transform.childCount != 0)
        {
            PlayerOne.transform.GetChild(0).gameObject.transform.parent = GameObject.Find("Book Container").transform;
        }

        for (int i = 0; i < PlayerTwo.transform.childCount; ++i)
        {

            Debug.Log("Books: " + i + " " + PlayerTwo.transform.GetChild(i).gameObject.tag);

            PlayerTwo.transform.GetChild(i).gameObject.SetActive(false);
            //reset tags and information
            PlayerTwo.transform.GetChild(i).gameObject.tag = "Book";
            PlayerTwo.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("BookLayer");
            PlayerTwo.transform.GetChild(i).gameObject.GetComponent<BookStackingScript>().touched = false;
            PlayerTwo.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            PlayerTwo.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }

        while (PlayerTwo.transform.childCount != 0)
        {
            PlayerTwo.transform.GetChild(0).gameObject.transform.parent = GameObject.Find("Book Container").transform;
        }



        //Finish running the game after its do
        GameEnd();
    }

	// Update logic for this minigame
    void Update()
    {
        //updating player movements
        //grounded = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));

        //float h = Input.GetAxis("Horizontal");

        if(!Finished && TimerOn)
        {
            if (TimeLeft <= 0)
            {
                Finished = true;
                Game_Finished();
            }
            else
            {
                TimeLeft -= Time.deltaTime;
                displayTime = Mathf.CeilToInt(TimeLeft);
                Timer.text = displayTime.ToString();
            }
        }
    }

    //Scoring Fucntions
    public void UpdateScore (int newScoreValue, int player_id)
    {
        //check for player id and update corresponding score
        if(player_id == 1)
        {
            player1ScoreText.text = "Player 1 Score: " + newScoreValue.ToString();
        }
        else
        {
            Debug.Log(PlayerTwoStats.MiniGameScore);
            player2ScoreText.text = "Player 2 Score: " + newScoreValue.ToString();
        }
    }

    //Spawns books that fall from the sky
    void spawnBook()
    {

        GameObject book = bookSpawner.GetPooledObject();
        book.SetActive(true);

        //Select random spawn point for book to drop
        int spawnPoint = UnityEngine.Random.Range(1, 10);

        book.transform.position = spawnPoints[spawnPoint].transform.position;
    }

    public override void UpTapAction(GameObject player)
    {

    }

    public override void LeftTapAction(GameObject player)
    {

    }

    public override void CenterTapAction(GameObject player)
    {
        if (!Finished)
        {
            //check to see if the player is on tomb and books carried is more than 1
            if (player.GetComponent<player_script>().isOnTomb && player.GetComponent<player_script>().BooksCarried > 0)
            {
                Debug.Log("I SHOULD BE IN");
                //update score
                int player_score = player.GetComponent<player_script>().BooksCarried;
                int player_id = player.GetComponent<player_script>().getPlayerID();

                Debug.Log(player.transform.childCount);
                //remove all the books
                for (int i = 0; i < player.transform.childCount; ++i)
                {

                    Debug.Log("Books: " + i + " " + player.transform.GetChild(i).gameObject.tag);

                    player.transform.GetChild(i).gameObject.SetActive(false);
                    //reset tags and information
                    player.transform.GetChild(i).gameObject.tag = "Book";
                    player.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("BookLayer");
                    player.transform.GetChild(i).gameObject.GetComponent<BookStackingScript>().touched = false;
                    player.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    player.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = true;

                }

                while (player.transform.childCount != 0)
                {
                    player.transform.GetChild(0).gameObject.transform.parent = GameObject.Find("Book Container").transform;
                }

                //update the score GUI
                player.GetComponent<player_script>().TotalScore += player_score;
                UpdateScore(player.GetComponent<player_script>().TotalScore, player_id);

                player.gameObject.layer = LayerMask.NameToLayer("PlayerLayer");
                player.GetComponent<player_script>().BooksCarried = 0;
            }
        }
    }

    public override void RightTapAction(GameObject player)
    {

    }

    public override void UpHeldAction(GameObject player)
    {
        //player.transform.Translate(0f, 0.1f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        if (!Finished)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + new Vector2(-8, 0) * Time.deltaTime);
            if (player.GetComponent<Rigidbody2D>().transform.childCount > 0)
            {
                player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(player.GetComponent<Rigidbody2D>().position.x,
                                                                                                            player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position.y);
            }
        }
    }

    public override void CenterHeldAction(GameObject player)
    {
        //player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        if (!Finished)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + new Vector2(8, 0) * Time.deltaTime);
            if (player.GetComponent<Rigidbody2D>().transform.childCount > 0)
            {
                player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(player.GetComponent<Rigidbody2D>().position.x,
                                                                                                            player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position.y);
            }
            //rigidbody2D.MovePosition(rigidbody2D.position + speed * Time.deltaTime);
        }
    }

    public override void UpRelAction(GameObject player)
    {
        
    }

    public override void LeftRelAction(GameObject player)
    {

    }

    public override void CenterRelAction(GameObject player)
    {

    }

    public override void RightRelAction(GameObject player)
    {

    }

   
}
