using UnityEngine;
using System.Collections;

public class BreakoutControl : Minigame
{

    GameObject PlayerOne;
    GameObject PlayerTwo;

    Stats PlayerOneStats;
    Stats PlayerTwoStats;

    public bool RaceStart = false;
    public Transform PlayerOneMove;
    public Transform PlayerTwoMove;
    public float P1Speed;
    public float P2Speed;
    public float TurnAngle;

    Vector3 Forward1;
    Vector3 Forward2;

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

        //Sets the controls, THIS MUST BE CALLED IN ORDER FOR CONTROLS TO WORK
        SetControls(PlayerOne);
        SetControls(PlayerTwo);


    }

    // Update is called once per frame
    void Update()
    {


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
        Vector2 v = new Vector2(0, 1);
        player.GetComponent<Rigidbody2D>().AddForce(v * P1Speed);
        //player.transform.Translate(0f, 0.5f, 0f);
    }

    public override void LeftHeldAction(GameObject player)
    {
        //player.transform.Translate(-0.5f, 0f, 0f);
    }

    public override void CenterHeldAction(GameObject player)
    {
        Vector2 v = new Vector2(0, -1);
        player.GetComponent<Rigidbody2D>().AddForce(v * P1Speed);
        //player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
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

    public override void OnStart()
    {
    }
    public void BGameEnd()
    {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag  == "ball")
        {
            Vector2 zero = new Vector2(0, 0);
            Vector2 EntryVector = coll.gameObject.GetComponent<Rigidbody2D>().velocity;
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = zero;
            

        }
    }
}
