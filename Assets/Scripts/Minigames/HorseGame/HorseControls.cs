﻿using UnityEngine;
using System.Collections;

public class HorseControls : Minigame {

    GameObject PlayerOne;
    GameObject PlayerTwo;

    Stats PlayerOneStats;
    Stats PlayerTwoStats;

    public bool RaceStart = false;
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

    // Use this for initialization
    void Start () {
        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;
        //Initialize time
        TimeLeft = 5;
        //Set player's positions/controls
        
        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);
        P1Speed = GameManager.Instance.Players[1].Value.Dex;
        P2Speed = GameManager.Instance.Players[2].Value.Dex;

}

    // Update is called once per frame
    void FixedUpdate () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RaceStart = !RaceStart;
        }
        if (RaceStart == true)
        {
            //PlayerOne.GetComponent<Rigidbody2D>().AddForce(Forward1 * P1Speed);
            //PlayerTwo.GetComponent<Rigidbody2D>().AddForce(Forward2 * P2Speed);
        }
	
	}
    public override void UpTapAction(GameObject player)
    {
        Debug.Log("Tapped the up key!");
    }

    public override void LeftTapAction(GameObject player)
    {
        //Quaternion Turn = Quaternion.Euler(0, 0, TurnAngle);
        

        Debug.Log("Tapped the left key!");
    }

    public override void CenterTapAction(GameObject player)
    {
        Debug.Log("Tapped the center key!");
    }

    public override void RightTapAction(GameObject player)
    {
        Debug.Log("Tapped the right key!");
    }

    public override void UpHeldAction(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().AddForce(player.transform.right * GameManager.Instance.Players[player.GetComponent<Stats>().Id].Value.Dex );
        Debug.Log(player.transform.forward);
        //player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
        rb.angularVelocity =   -tf;
        //player.transform.Translate(-0.5f, 0f, 0f);
    }

    public override void CenterHeldAction(GameObject player)
    {
        //player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
        rb.angularVelocity = tf;
        //player.transform.Translate(0.5f, 0f, 0f);
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
    }

}
