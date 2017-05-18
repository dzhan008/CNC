using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BlacksmithMinigame : Minigame {

    private GameObject PlayerOne;
    private GameObject PlayerTwo;

    private Stats PlayerOneStats;
    private Stats PlayerTwoStats;

    public GameObject HitPoint;
    private HitPoint Point;

    public Text PlayerOneScore;
    public Text PlayerTwoScore;
    public Text Timer;

    AudioSource audio;

    List<GameObject> Weapons;

    int CurrentWeapon = 0;

    // Use this for initialization
    void Start() {

        audio = GetComponent<AudioSource>();
        audio.time = 20;

        PlayerOne = GameManager.Instance.Players[1].Key;
        PlayerTwo = GameManager.Instance.Players[2].Key;

        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;

        //Set player's positions/controls
        PlayerOne.transform.position = new Vector3(-25f, 5f, 0f);
        PlayerTwo.transform.position = new Vector3(-25f, 5f, 0f);

        SetControls(PlayerOne);
        SetControls(PlayerTwo);

        SetTime(3);

        Weapons = new List<GameObject>();
        Point = HitPoint.GetComponent<HitPoint>();
        InitWeapons();

    }

    // Update is called once per frame
    void Update() {

        if (Weapons[CurrentWeapon].GetComponent<Weapon>().Completed)
        {
            SelectWeapon();
            Weapons[CurrentWeapon].GetComponent<Weapon>().ResetWeapon();
        }
        if (!Finished && TimerOn)
        {
            if (CountDown(1) != 0)
            {
                Finished = true;
                StartCoroutine(EndGame(2));
            }
            else
            {
                Timer.text = "Time Left: " + (int)TimeLeft;
            }
        }

    }

    public override void OnStart()
    {
        GameManager.Instance.GameState = States.InGame;
        StartCoroutine(StartGame(1));
    }

    IEnumerator EndGame(float time)
    {
        if(PlayerOneStats.MiniGameScore > PlayerTwoStats.MiniGameScore)
        {
            Timer.text = "Player 1 Wins!";
        }
        else if(PlayerOneStats.MiniGameScore < PlayerTwoStats.MiniGameScore)
        {
            Timer.text = "Player 2 Wins!";
        }
        else
        {
            Timer.text = "It's a tie!";
        }
        yield return new WaitForSeconds(time);
        GameEnd();
        
    }

    IEnumerator StartGame(float time)
    {
        UIManager.Instance.FadeIn();
        yield return new WaitForSeconds(time);
        UIManager.Instance.FadeOut();
        InstructionPanel.SetActive(false);
        yield return new WaitForSeconds(time);
        SelectWeapon();
        TimerOn = true;
    }
    void InitWeapons()
    {
        Weapons.Add((GameObject)Instantiate(Resources.Load("Prefabs/Minigames/Swift Smiths/Axe")));
        Weapons.Add((GameObject)Instantiate(Resources.Load("Prefabs/Minigames/Swift Smiths/Spear")));
        Weapons.Add((GameObject)Instantiate(Resources.Load("Prefabs/Minigames/Swift Smiths/Sword")));
        for(int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].transform.parent = this.transform;
        }
    }

    void SelectWeapon()
    {
        Debug.Log("Making Weapons!");
        Weapons[CurrentWeapon].SetActive(false);
        CurrentWeapon = Random.Range(0, Weapons.Count);
        Weapons[CurrentWeapon].SetActive(true);
    }

    public override void UpTapAction(GameObject player)
    {
        if (Point.InZone(player) && !Finished)
        {
            //Debug.Log("I hit ya!");
            Point.Damage(10);
            //TODO: Change this to some sensible score.
            Debug.Log(Mathf.Abs(player.transform.position.y - HitPoint.transform.position.y));

            float difference = Mathf.Abs(player.transform.position.y - HitPoint.transform.position.y);
            int diff = (int)(100 * difference);
            int result = 100 - diff;

            if (player.GetComponent<Stats>().Id == 1)
            {
                player.GetComponent<Stats>().MiniGameScore += result;
                PlayerOneScore.text = "Player One Score: " + player.GetComponent<Stats>().MiniGameScore.ToString();
            }
            else
            {
                player.GetComponent<Stats>().MiniGameScore += result;
                PlayerTwoScore.text = "Player Two Score: " + player.GetComponent<Stats>().MiniGameScore.ToString();
            }

            if (player.transform.position.y == HitPoint.transform.position.y)
            {
                Debug.Log("Perfect Hit!");
            }
        }
        else
        {
            Debug.Log("JOKES ON YOU BUDDY YOU MISSED");
        }
    }

    public override void LeftTapAction(GameObject player)
    {}

    public override void CenterTapAction(GameObject player)
    {}

    public override void RightTapAction(GameObject player)
    {}

    public override void UpHeldAction(GameObject player)
    {}

    public override void LeftHeldAction(GameObject player)
    {
        Rigidbody2D p_rigidbody = player.GetComponent<Rigidbody2D>();
        Vector2 desired_pos = new Vector2(0, 5f * Time.deltaTime);
        p_rigidbody.MovePosition(p_rigidbody.position + desired_pos);
    }

    public override void CenterHeldAction(GameObject player)
    {
       // player.transform.Translate(0f, -0.5f, 0f);
    }

    public override void RightHeldAction(GameObject player)
    {
        Rigidbody2D p_rigidbody = player.GetComponent<Rigidbody2D>();
        Vector2 desired_pos = new Vector2(0, -5f * Time.deltaTime);
        p_rigidbody.MovePosition(p_rigidbody.position + desired_pos);
    }

    public override void UpRelAction(GameObject player)
    {}

    public override void LeftRelAction(GameObject player)
    {}

    public override void CenterRelAction(GameObject player)
    {}

    public override void RightRelAction(GameObject player)
    {}
}
