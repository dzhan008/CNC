using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DragonMiniGame : Minigame
{
    [SerializeField]
    private CockBlockSpawn CBPlayerOne;
    [SerializeField]
    private CockBlockSpawn CBPlayerTwo;
    [SerializeField]
    private BarScript ProgressBar;

    [SerializeField]
    private float _ProgressDistVal;
    public float ProgressDistVal
    {
        get
        {
            return _ProgressDistVal;
        }
        set
        {
            _ProgressDistVal = Mathf.Clamp(value, 0, ProgressMaxVal);
            ProgressBar.Value = _ProgressDistVal;
        }
    }
    [SerializeField]
    private float _ProgressMaxVal;
    public float ProgressMaxVal
    {
        get
        {
            return _ProgressMaxVal;
        }

        set
        {//set the max value of the bar
            this._ProgressMaxVal = value;
            ProgressBar.MaxValue = _ProgressMaxVal;

        }
    }

    [SerializeField]
    private Canvas CanvasUI;
    public Text GameOverText;
    GameObject PlayerOne;
    GameObject PlayerTwo;
    [SerializeField]
    private Vector3 offset;
    private Camera MainCamera;
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
    private bool _IsGameEnd = false;
    public bool IsGameEnd { get; set; }
    private bool EndingGame = false;
    private int _Winner = -1;
    public int Winner { get; set; }
    public float playerDrag = 5;
    bool _StartGame = false;
    public bool StartGame { get; set; }

    public GameObject HHDCamera;
    private Vector3 OrigCameraPos;

    public GameObject PlayerOneSpawnPoint;
    public GameObject PlayerTwoSpawnPoint;
    public GameObject Dragon;

    public float numPacePoints;
    private float increasePacePoint;
    private float numGapSpace;
    public float increasePace;

    public float increaseObstacleSlow;
    /*Contains the skills/abilities value of each player*/

    public override void OnStart()
    {
        InstructionPanel.SetActive(false);
        StartCoroutine(StartTimer());
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
    IEnumerator StartTimer()
    {
        //Start Timer
        int count = 5;
        while (count > 0)
        {
            string text = "Game starts in " + count.ToString() + ".";
            GameOverText.text = text;
            count--;
            yield return new WaitForSeconds(1);
        }
        GameOverText.text = "Go!";
        yield return new WaitForSeconds(1);
        GameOverText.text = "";
        StartGame = true;
        //Game Starts
        ProgressMaxVal = findEndDist();
        numGapSpace = ProgressMaxVal / numPacePoints;
        increasePacePoint = numGapSpace;
        GameManager.Instance.GameState = States.InGame;
        PlayerOne.GetComponentInChildren<Animator>().SetFloat("Running", 1);
        PlayerTwo.GetComponentInChildren<Animator>().SetFloat("Running", 1);
        Dragon.GetComponent<Animator>().SetFloat("Running", 1);
    }
    void Start()
    {
        AudioManager.Instance.PlaySong("Hungry Hungry Dragon");
        MainCamera = Camera.main;
        HHDCamera.transform.parent = MainCamera.transform;
        CanvasUI.worldCamera = Camera.main;
        OrigCameraPos = Camera.main.transform.position;

        //Initialize time
        //TimerOn = false;
        //TimeLeft = 5000;
        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;

        PlayerOneStats.SetPerspective(Perspective);
        PlayerTwoStats.SetPerspective(Perspective);

        //Initialize time
        TimeLeft = 5;
        //Set player's positions/controls
        PlayerOne.transform.position = PlayerOneSpawnPoint.transform.position;
        PlayerTwo.transform.position = PlayerTwoSpawnPoint.transform.position;

        //Add Scripts for my Game
        PlayerOne.AddComponent<PlayerStat>();
        PlayerTwo.AddComponent<PlayerStat>();
        PlayerOne.AddComponent<PlayerCollision>();
        PlayerTwo.AddComponent<PlayerCollision>();

        //Update Camera
        float offset = 0.8f;
        //find the new x position of the camera to be in the middle of two players
        float posX = Mathf.SmoothDamp(MainCamera.transform.position.x,
            MidPointFormula() + offset, ref CameraVelocity.x, SmoothTimeX);
        //Change the camera's position
        MainCamera.transform.position = new Vector3(posX, 5f,
            MainCamera.transform.position.z);
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);
        PlayerOne.GetComponent<PlayerStat>().Initialize(PlayerOneStats, PlayerOneSprintBar, PlayerOneObstacleBar, CBPlayerOne, 15, 15, 3, 3);
        PlayerTwo.GetComponent<PlayerStat>().Initialize(PlayerTwoStats, PlayerTwoSprintBar, PlayerTwoObstacleBar, CBPlayerTwo, 15, 15, 3, 3);
        PlayerOne.GetComponent<PlayerCollision>().Initialize(1, PlayerOne.GetComponent<PlayerStat>());
        PlayerTwo.GetComponent<PlayerCollision>().Initialize(2, PlayerTwo.GetComponent<PlayerStat>());
        PlayerOne.GetComponent<Rigidbody2D>().isKinematic = false;
        PlayerTwo.GetComponent<Rigidbody2D>().isKinematic = false;
        PlayerOne.GetComponent<Rigidbody2D>().gravityScale = 3;
        PlayerTwo.GetComponent<Rigidbody2D>().gravityScale = 3;
        PlayerOne.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        PlayerTwo.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        PlayerOne.layer = LayerMask.NameToLayer("LayerC");
        PlayerTwo.layer = LayerMask.NameToLayer("LayerC");
    }

    float MidPointFormula()
    {
        return (PlayerOne.transform.position.x + PlayerTwo.transform.position.x) / 2;
    }
    void updateCamera()
    {
        GameObject roadSpawner = GameObject.Find("RoadSpawner");
        if (MainCamera.transform.position.x > (roadSpawner.transform.position.x - 7))
        {
            return;
        }
        float offset = 0.9f;
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
                player.GetComponent<PlayerStat>().PSkills["sprintSpeedAdd"] = 0.07f;
                player.GetComponent<PlayerStat>().PSkills["sprintStartTime"] = playerSprintStart;
            }
        }
        //if the player isn't sprinting and their sprint bar isn't full
        else if (player.GetComponent<PlayerStat>().SprintCurrentVal != player.GetComponent<PlayerStat>().SprintMaxVal
            && playerIsSprint != 1)
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
            player.GetComponent<PlayerStat>().ObstacleCurrentVal += (5 * Time.deltaTime);
        }
    }
    float findEndDist()
    {
        //check who's in lead first
        GameObject temp;
        if (PlayerOne.transform.position.x > PlayerTwo.transform.position.x) temp = PlayerOne;
        else temp = PlayerTwo;
        GameObject endPoint = GameObject.Find("RoadSpawner");
        float distance = endPoint.transform.position.x - temp.transform.position.x;
        return distance;
    }
    void updateProgressBar(GameObject player1, GameObject player2)
    {
        ProgressDistVal = ProgressMaxVal - findEndDist();
        if (ProgressDistVal >= increasePacePoint)
        {
            player1.GetComponent<PlayerStat>().PSkills["baseSpeed"] += increasePace;
            player2.GetComponent<PlayerStat>().PSkills["baseSpeed"] += increasePace;
            increasePacePoint += numGapSpace;
            player1.GetComponent<PlayerStat>().PSkills["speedReduction"] += increaseObstacleSlow;
            player2.GetComponent<PlayerStat>().PSkills["speedReduction"] += increaseObstacleSlow;
        }
    }
    IEnumerator EndCinematic(GameObject winner, GameObject loser)
    {
        GameManager.Instance.GameState = States.Results;
        Dragon.GetComponent<Animator>().SetFloat("Running", 0);
        while (Dragon.transform.position.x < loser.transform.position.x - 1)
        {
            Dragon.transform.Translate(0.001f, 0f, 0f);
            winner.transform.Translate(0.01f, 0f, 0f);
            if (Dragon.transform.position.y != loser.transform.position.y - 1)
            {
                if (Dragon.transform.position.y < loser.transform.position.y - 1)
                {
                    Dragon.transform.Translate(0f, 0.001f, 0f);
                }
                if (Dragon.transform.position.y > loser.transform.position.y - 1)
                {
                    Dragon.transform.Translate(0f, -0.001f, 0f);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3);
        Destroy(GameObject.Find("Road1 Container"));
        Destroy(GameObject.Find("LastTile"));
        Destroy(GameObject.Find("GoalWall"));
        Destroy(GameObject.Find("HHDCamera"));
        Destroy(GameObject.Find("Obstacle Container"));
        Destroy(GameObject.Find("chicken Container"));
        PlayerOne.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerTwo.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerOne.GetComponent<Rigidbody2D>().isKinematic = true;
        PlayerTwo.GetComponent<Rigidbody2D>().isKinematic = true;
        IsGameEnd = false;
        EndingGame = true;
        PlayerOne.transform.localScale = new Vector3(1f, 1f, 1f);
        PlayerTwo.transform.localScale = new Vector3(1f, 1f, 1f);
        PlayerOne.layer = LayerMask.NameToLayer("Player");
        PlayerTwo.layer = LayerMask.NameToLayer("Player");
        Camera.main.transform.position = OrigCameraPos;
    }
    void myGameEnd(GameObject winner, GameObject loser)
    {
        PlayerOne.GetComponentInChildren<Animator>().SetFloat("Running", 0);
        PlayerTwo.GetComponentInChildren<Animator>().SetFloat("Running", 0);
        StartCoroutine(EndCinematic(winner, loser));
        Destroy(PlayerOne.GetComponent<PlayerCollision>());
        Destroy(PlayerTwo.GetComponent<PlayerCollision>());
        Destroy(PlayerOne.GetComponent<PlayerStat>());
        Destroy(PlayerTwo.GetComponent<PlayerStat>());


    }
    // Update logic for this minigame
    void Update()
    {
        if (IsGameEnd)
        {
            StartGame = false;
            if (Winner == 1) myGameEnd(PlayerOne, PlayerTwo); //make it so that it only calls once 
            else myGameEnd(PlayerTwo, PlayerOne);
        }
        else if (EndingGame)
        {
            GameEnd();
        }
        if (StartGame)
        {
            //Debug.Log("asdf");
            updateSpeed(PlayerOne);
            updateSpeed(PlayerTwo);
            updateSprint(PlayerOne);
            updateSprint(PlayerTwo);
            updateCockBlock(PlayerOne);
            updateCockBlock(PlayerTwo);
            updateCamera();
            updateProgressBar(PlayerOne, PlayerTwo);
        }
    }

    public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }

    public override void LeftTapAction(GameObject player)
    {
        float jump_height = 0;
        jump_height = player.GetComponent<PlayerStat>().PSkills["jumpHeight"];
        if (player.GetComponent<PlayerCollision>().CanJump)
        {
            player.GetComponent<PlayerCollision>().CanJump = false;
            player.GetComponent<Rigidbody2D>().AddForce(player.transform.up * jump_height);
        }
    }

    public override void CenterTapAction(GameObject player)
    {
        float max = player.GetComponent<PlayerStat>().SprintMaxVal;
        float current = player.GetComponent<PlayerStat>().SprintCurrentVal;
        if (player.GetComponent<PlayerStat>().PSkills["isSprint"] == 0 &&
            current == max)
        {
            player.GetComponent<PlayerStat>().SprintCurrentVal -= max;
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

}

