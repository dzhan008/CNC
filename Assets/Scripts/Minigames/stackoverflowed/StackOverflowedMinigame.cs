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

    /*Player BookHolders */
    public GameObject PlayerOneBookHolder;
    public GameObject PlayerTwoBookHolder;
    
    //-----Instructions-----//
    public bool inInstructions = true;

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
        // CREATE INSTRUCTION STATE---------------------------------TODO: Start timer in on start
        AudioManager.Instance.PlaySong("Stack Overflowed");
        Debug.Log("Minigame Initializing!");
        //Initialize time and set score to 0
        TimeLeft = 30;
        //disbale finished text until game is won
        finishedText.enabled = false;

        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;

        PlayerOneStats.SetPerspective(Perspective);
        PlayerTwoStats.SetPerspective(Perspective);

        PlayerOneStats.MiniGameScore = 0;
        PlayerTwoStats.MiniGameScore = 0;

        PlayerOne.transform.position = SpawnPoint1.transform.position;
        PlayerTwo.transform.position = SpawnPoint2.transform.position;

        PlayerOneBookHolder.transform.position = SpawnPoint1.transform.position;
        PlayerTwoBookHolder.transform.position = SpawnPoint2.transform.position;

        PlayerOneBookHolder.transform.parent = PlayerOne.transform;
        PlayerTwoBookHolder.transform.parent = PlayerTwo.transform;
        
        PlayerOne.AddComponent<PlayerScript>();
        PlayerTwo.AddComponent<PlayerScript>();


        //object pooling stuff
        bookSpawner = this.GetComponent<ObjectPooler>();
        spawnRate = Time.time + addTime;
        
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);

        PlayerOne.layer = LayerMask.NameToLayer("Player");
        PlayerTwo.layer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreCollision(PlayerOne.GetComponent<Collider2D>(), PlayerTwo.GetComponent<Collider2D>());  
    }

    public override void OnStart()
    {
        Debug.Log("I'm playing");
        InstructionPanel.SetActive(false);
        TimerOn = true;
        InvokeRepeating("spawnBook", 1.0f, 0.5f);
        inInstructions = false;
        GameManager.Instance.GameState = States.InGame;
    }

    //Game is finished complete the game now :D
    void Game_Finished()
    {
        //disable player rigid body to stop all movmenet
        finishedText.enabled = true;

        //stop all the invokes spawning books
        CancelInvoke();

        GameManager.Instance.GameState = States.Results;

        Destroy(PlayerOneBookHolder);
        Destroy(PlayerTwoBookHolder);

        Destroy(PlayerOne.GetComponent<PlayerScript>());
        Destroy(PlayerTwo.GetComponent<PlayerScript>());

        Destroy(GameObject.Find("Book Container"));

        //Check to see who is the winner
        //delay for a little bit then show the winner
        //update minigamescore because i did not use that variable xP
        PlayerOneStats.MiniGameScore = PlayerOne.GetComponent<PlayerScript>().TotalScore;
        PlayerTwoStats.MiniGameScore = PlayerTwo.GetComponent<PlayerScript>().TotalScore;

        PlayerOne.GetComponentInChildren<Animator>().SetFloat("Running", 0);
        PlayerTwo.GetComponentInChildren<Animator>().SetFloat("Running", 0);

        StartCoroutine(DecideWinner());
    }

    //this coroutine is to pause for a little bit after finished is displayed
    IEnumerator DecideWinner()
    {

        //pause 3 seconds
        yield return new WaitForSeconds(2);

        if (PlayerOneStats.MiniGameScore > PlayerTwoStats.MiniGameScore)
        {
            finishedText.text = "Player One Wins!";
        }
        else if (PlayerOneStats.MiniGameScore < PlayerTwoStats.MiniGameScore)
        {
            finishedText.text = "Player Two Wins!";
        }
        else
        {
            finishedText.text = "Tie!";
        }

        yield return new WaitForSeconds(3);
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
        Vector2 side = player.transform.localScale;
        side.x = (player.transform.localScale.x < 0f) ? -1 * player.transform.localScale.x : player.transform.localScale.x;
        player.transform.localScale = side;
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
    }

    public override void CenterTapAction(GameObject player)
    {
        if (!Finished && !inInstructions)
        {
            Debug.Log(player.GetComponent<PlayerScript>().isOnTomb);
            //check to see if the player is on tomb and books carried is more than 1
            if (player.GetComponent<PlayerScript>().isOnTomb && player.GetComponent<PlayerScript>().BooksCarried > 0)
            {
                Debug.Log("I SHOULD BE IN");
                //update score
                int player_score = player.GetComponent<PlayerScript>().BooksCarried;
                int player_id = player.GetComponent<PlayerScript>().getPlayerID();

                Transform book_holder = player.transform.Find("BookHolder");

                //remove all the books
                for (int i = 0; i < book_holder.childCount; ++i)
                {

                    Debug.Log("Books: " + i + " " + book_holder.GetChild(i).gameObject.tag);
                    GameObject book = book_holder.GetChild(i).gameObject;
                    if(book.tag == "BookTouched" || book.tag == "BookTop")
                    {
                        //reset tags and information
                        book.tag = "Book";
                        book.layer = LayerMask.NameToLayer("BookLayer");
                        book.GetComponent<BookStackingScript>().touched = false;
                        book.GetComponent<Rigidbody2D>().isKinematic = false;
                        book.GetComponent<BoxCollider2D>().enabled = true;
                        book.gameObject.SetActive(false);
                    }


                }

                //Should probably fix this. This ASSUMES that the model is the first child, so we know that new books added are under it.
                while (book_holder.childCount != 0)
                {
                        book_holder.GetChild(0).gameObject.transform.parent = GameObject.Find("Book Container").transform;
                }

                //update the score GUI
                player.GetComponent<PlayerScript>().TotalScore += player_score;
                UpdateScore(player.GetComponent<PlayerScript>().TotalScore, player_id);

                player.gameObject.layer = LayerMask.NameToLayer("Player");
                player.GetComponent<PlayerScript>().BooksCarried = 0;
            }
        }
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
        //player.transform.Translate(0f, 0.1f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
        if (!Finished && !inInstructions)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + new Vector2(-8, 0) * Time.deltaTime);
            if (player.GetComponent<Rigidbody2D>().transform.childCount > 0)
            {
                Transform possible_book = player.transform.Find("Book(Clone)");
                if(possible_book != null)
                possible_book.gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(player.GetComponent<Rigidbody2D>().position.x,
                                                                                                            possible_book.gameObject.GetComponent<Rigidbody2D>().transform.position.y);
            }
        }
    }

    public override void CenterHeldAction(GameObject player)
    {
        //player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        player.GetComponentInChildren<Animator>().SetFloat("Running", 1);
        if (!Finished && !inInstructions)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + new Vector2(8, 0) * Time.deltaTime);
            if (player.GetComponent<Rigidbody2D>().transform.childCount > 0)
            {
                Transform possible_book = player.transform.Find("Book(Clone)");
                if (possible_book != null)
                possible_book.gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(player.GetComponent<Rigidbody2D>().position.x,
                                                                                                            possible_book.gameObject.GetComponent<Rigidbody2D>().transform.position.y);
            }
            //rigidbody2D.MovePosition(rigidbody2D.position + speed * Time.deltaTime);
        }
    }

    public override void UpRelAction(GameObject player)
    {
        
    }

    public override void LeftRelAction(GameObject player)
    {
        player.GetComponentInChildren<Animator>().SetFloat("Running", 0);
    }

    public override void CenterRelAction(GameObject player)
    {

    }

    public override void RightRelAction(GameObject player)
    {
        player.GetComponentInChildren<Animator>().SetFloat("Running", 0);
    }

   
}
