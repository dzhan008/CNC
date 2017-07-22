using UnityEngine;
using System.Collections;
using System;

public class HorseControls : Minigame
{

    GameObject PlayerOne;
    GameObject PlayerTwo;

    Stats PlayerOneStats;
    Stats PlayerTwoStats;

    public bool RaceStart = false;
    public bool RaceEnd = false;
    public Transform PlayerOneMove;
    public Transform PlayerTwoMove;
    public float P1Speed;
    public float P2Speed;
    public float torqueForce = -100f; //turn speed
    float driftFactorSticky = 0.9f;
    float driftFactorSlippy = 1;
    float maxStickyVelocity = 2.5f;
    Vector3 Forward1;
    Vector3 Forward2;
    public float Countdown;
    [SerializeField] GameObject map;
    public Transform start;
    [SerializeField] GameObject start1;
    [SerializeField] GameObject start2;
    // Use this for initialization
    void Start()
    {
        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;
        //Initialize time
        TimeLeft = 5;
        //Set player's positions/controls
        GameManager.Instance.GameState = States.InGame;
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);
        P1Speed = GameManager.Instance.Players[1].Value.Dex;
        P2Speed = GameManager.Instance.Players[2].Value.Dex;
        map = GameObject.FindGameObjectWithTag("Map");
        start = map.gameObject.transform.FindChild("StartLine");
        start1 = GameObject.Find("Player1Start");
        start2 = GameObject.Find("Player2Start");
        PlayerOne.transform.position = start1.transform.position;
        PlayerTwo.transform.position = start2.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Countdown <= 0)
        {
            RaceStart = true;
        }
        Countdown -= Time.deltaTime;
    }
    public override void UpTapAction(GameObject player)
    {
        //Debug.Log("Tapped the up key!");
    }

    public override void LeftTapAction(GameObject player)
    {
        //Quaternion Turn = Quaternion.Euler(0, 0, TurnAngle);


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
        float MaxSpeed = GameManager.Instance.Players[player.GetComponent<Stats>().Id].Value.Str * 2;
        Vector2 MaxVelocity = player.transform.up * MaxSpeed;
        //Debug.Log("Player Veloc " + MaxVelocity.magnitude);
        Vector2 Acceleration = player.transform.up * GameManager.Instance.Players[player.GetComponent<Stats>().Id].Value.Dex;
        //Debug.Log("Player Accel " + Acceleration.magnitude);
        if (RaceStart && !RaceEnd)
        {
            //player.GetComponent<Rigidbody2D>().AddForce(player.transform.right * GameManager.Instance.Players[player.GetComponent<Stats>().Id].Value.Dex);
            //Debug.Log(player.transform.forward);
            //Debug.Log("Accelleration " + Acceleration.magnitude);
            player.GetComponent<Rigidbody2D>().velocity += Acceleration * Time.deltaTime;
            //Debug.Log(player.GetComponent<Rigidbody2D>().velocity.magnitude);
            if (player.GetComponent<Rigidbody2D>().velocity.magnitude > MaxSpeed)
            {
                player.GetComponent<Rigidbody2D>().velocity = MaxVelocity;
            }
        }
        //player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        Debug.Log("Up Pressed");
        if (RaceStart && !RaceEnd)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
            rb.angularVelocity = (-tf / 10)  * player.GetComponent<Stats>().Dex;
        }
        //player.transform.Translate(-0.5f, 0f, 0f);
    }

    public override void CenterHeldAction(GameObject player)
    {
        float MaxSpeed = GameManager.Instance.Players[2].Value.Dex;
        Vector2 Acceleration = -player.transform.right;
        if (RaceStart && !RaceEnd)
        {
            player.GetComponent<Rigidbody2D>().AddForce(-player.transform.right * GameManager.Instance.Players[player.GetComponent<Stats>().Id].Value.Dex);
            player.GetComponent<Rigidbody2D>().velocity += Acceleration * Time.fixedDeltaTime;
            //Debug.Log(player.GetComponent<Rigidbody2D>().velocity);
        }
    }

    public override void RightHeldAction(GameObject player)
    {
        if (RaceStart && !RaceEnd)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
            rb.angularVelocity = (tf / 10)  * player.GetComponent<Stats>().Dex;
        }
        //player.transform.Translate(0.5f, 0f, 0f);
    }

    public override void UpRelAction(GameObject player)
    {
        //Debug.Log("Released the up key!");
    }

    public override void LeftRelAction(GameObject player)
    {
        if (RaceStart && !RaceEnd)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
            //Debug.Log("Released the left key!");
        }
    }

    public override void CenterRelAction(GameObject player)
    {
        //Debug.Log("Released the center key!");
    }

    public override void RightRelAction(GameObject player)
    {
        if (RaceStart && !RaceEnd)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
            //Debug.Log("Released the right key!");
        }
    }
    public override void OnStart()
    {
    }
    public void HGameEnd()
    {
        Debug.Log("Game End");
        RaceEnd = true;
        PlayerOne.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        PlayerTwo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        while(this.GetComponent<HGameUI>().running)
        {

        }
        GameEnd();
    }

}