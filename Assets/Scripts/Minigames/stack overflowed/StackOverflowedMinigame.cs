/*
Created By: David Zhang
Description: Sample script to handle start/update/end minigame logic in one script
Requirements: A minigame prefab.
*/

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

    //create variables to set player physics------Probably move this to another file
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundChecker;

    private bool grounded = false;
    private Rigidbody2D rb2d;   //------May or may not need


    //--------Scoring-----------//
    public GUIText scoreText;
    public int score;

    //Used to display the timer, if needed.
    public Text Timer;
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
        //Initialize time
        TimerOn = false;
        TimeLeft = 5;

        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;

        PlayerOne.transform.position = SpawnPoint1.transform.position;
        PlayerTwo.transform.position = SpawnPoint2.transform.position;

        //Set player's positions/controls
        //PlayerOne.transform.position = new Vector3(-30f, 5f, 0f);
        //PlayerTwo.transform.position = new Vector3(-20f, 5f, 0f);

        //object pooling stuff
        bookSpawner = this.GetComponent<ObjectPooler>();
        spawnRate = Time.time + addTime;
        InvokeRepeating("spawnBook", 1.0f, 0.5f);
        UpdateText();

        Debug.Log(PlayerOneStats.Intel);
        Debug.Log(PlayerOneStats.Str);
        Debug.Log(PlayerOneStats.Dex);

        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);

   
    }

	// Update logic for this minigame
    void Update()
    {
        //updating player movements
        //grounded = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));

        //float h = Input.GetAxis("Horizontal");

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

    //Scoring Fucntions
    void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateText();
    }

    void UpdateText() {
        scoreText.text = "Score: " +  score;
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
        player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + new Vector2(-8, 0) * Time.deltaTime);
        if (player.GetComponent<Rigidbody2D>().transform.childCount > 0)
        {
            player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(player.GetComponent<Rigidbody2D>().position.x,
                                                                                                        player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position.y);
        }
    }

    public override void CenterHeldAction(GameObject player)
    {
        //player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + new Vector2(8, 0) * Time.deltaTime);
        if (player.GetComponent<Rigidbody2D>().transform.childCount > 0)
        {
            player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(player.GetComponent<Rigidbody2D>().position.x,
                                                                                                        player.transform.Find("Book(Clone)").gameObject.GetComponent<Rigidbody2D>().transform.position.y);
        }
        //rigidbody2D.MovePosition(rigidbody2D.position + speed * Time.deltaTime);
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
