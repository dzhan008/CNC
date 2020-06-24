using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HuntingMinigame : Minigame {

	private GameObject PlayerOne;
	private GameObject PlayerTwo;
	private Stats PlayerOneStats;
	private Stats PlayerTwoStats;
    private HuntingObjectPooler Pooler;
    private HuntingObjectPooler Pooler1;
    public GameObject Generator;
    public GameObject Generator1;
    public Transform PlayerOneSpawn;
    public Transform PlayerTwoSpawn;
	public Text scoreP1;
	public Text scoreP2;
	float timeL = 45.00f;
	public Text timeT;
    public Text winner;
    float Fire1 = 1f;
    float Fire2 = 1f;
    float Fire1Max;
    float Fire2Max;
    public GameObject Bull;
    public GameObject Bull1;
    public bool gameStart = false;
    // Use this for initialization
    void Start () 
	{
        AudioManager.Instance.PlaySong("Hunting");
        PlayerOne = GameManager.Instance.Players [1].Key;
		PlayerTwo = GameManager.Instance.Players [2].Key;
		PlayerOneStats = GameManager.Instance.Players [1].Value;
		PlayerTwoStats = GameManager.Instance.Players [2].Value;
		SetControls (PlayerOne);
		SetControls (PlayerTwo);

        PlayerOneStats.SetPerspective(Perspective);
        PlayerTwoStats.SetPerspective(Perspective);

        PlayerOne.transform.position = PlayerOneSpawn.position;
        PlayerTwo.transform.position = PlayerTwoSpawn.position;

        SetPlayerStats();

        //TODO: Move this to the instructions screen!
    }

    private void SetPlayerStats()
    {
        //Dex Check (Fire Rate)
        Fire1 = 0;
        Fire2 = 0;

        Fire1Max = 1f - ((PlayerOne.GetComponent<Stats>().Dex) * .07f);
        Fire2Max = 1f - ((PlayerTwo.GetComponent<Stats>().Dex) * .07f);

        //Int Check (Arrow Speed)
        Bull.GetComponent<Bullet>().speed = ((PlayerOne.GetComponent<Stats>().Intel) * 0.15f);
        Bull1.GetComponent<Bullet1>().speed = ((PlayerTwo.GetComponent<Stats>().Intel) * 0.15f);

        Debug.Log(Fire1Max);
        Debug.Log(Fire2Max);
        //Player One Str Check (Arrow Damage)
        if (PlayerOne.GetComponent<Stats>().Str >= 10)
        {
            Bull.GetComponent<Bullet>().damage = 250;
        }
        else if (PlayerOne.GetComponent<Stats>().Str > 6 || PlayerOne.GetComponent<Stats>().Str <= 9)
        {
            Bull.GetComponent<Bullet>().damage = 167;
        }
        else if (PlayerOne.GetComponent<Stats>().Str > 3 || PlayerOne.GetComponent<Stats>().Str <= 6)
        {
            Bull.GetComponent<Bullet>().damage = 125;
        }
        else if (PlayerOne.GetComponent<Stats>().Str >= 1 || PlayerOne.GetComponent<Stats>().Str <= 3)
        {
            Bull.GetComponent<Bullet>().damage = 100;
        }

        //Player Two Str Check
        if (PlayerTwo.GetComponent<Stats>().Str >= 10)
        {
            Bull1.GetComponent<Bullet1>().damage = 250;
        }
        else if (PlayerTwo.GetComponent<Stats>().Str > 6 || PlayerTwo.GetComponent<Stats>().Str <= 9)
        {
            Bull1.GetComponent<Bullet1>().damage = 167;
        }
        else if (PlayerTwo.GetComponent<Stats>().Str > 3 || PlayerTwo.GetComponent<Stats>().Str <= 6)
        {
            Bull1.GetComponent<Bullet1>().damage = 125;
        }
        else if (PlayerTwo.GetComponent<Stats>().Str >= 1 || PlayerTwo.GetComponent<Stats>().Str <= 3)
        {
            Bull1.GetComponent<Bullet1>().damage = 100;
        }
    }

    // Update is called once per frame
    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(5);
        Destroy(GameObject.Find("BulletOneContainer"));
        Destroy(GameObject.Find("BulletContainer"));
        Destroy(GameObject.Find("MonsterContainer"));
        GameEnd();
    }

    void Update () 
	{
		if (gameStart == true)
        {
            timeL -= Time.deltaTime;
            if (timeL <= 0)
            {
                timeL = 0;
                MyGameEnd();
                StartCoroutine(MyCoroutine());
            }
            timeT.text = "Time: " + (int)timeL;
            Fire1 += Time.deltaTime;
            Fire2 += Time.deltaTime;
        }
    }

    public override void OnStart()
    {
        GameManager.Instance.GameState = States.InGame;
        InstructionPanel.SetActive(false);
        Pooler = Generator.GetComponent<HuntingObjectPooler>();
        Pooler1 = Generator1.GetComponent<HuntingObjectPooler>();
        gameStart = true;
    }



    public void SetScoreP1 () {
		scoreP1.text = "Score: " + GameManager.Instance.Players [1].Value.MiniGameScore.ToString ();
	}

	public void SetScoreP2 () {
		scoreP2.text = "Score: " + GameManager.Instance.Players [2].Value.MiniGameScore.ToString ();
	}

	public override void UpTapAction(GameObject player)
	{
        if(player == PlayerOne && Fire1 > Fire1Max)
        {
            PlayerOne.GetComponentInChildren<Animator>().SetTrigger("Fire");
            FireArrow(player);
        }
        if (player == PlayerTwo && Fire2 > Fire2Max)
        {
            PlayerTwo.GetComponentInChildren<Animator>().SetTrigger("Fire");
            FireArrow(player);
        }

    }

    public void FireArrow(GameObject player)
    {
        if (player == PlayerOne)
        {
            Transform BulletGenPoint = PlayerOne.GetComponent<Stats>().CurrentModel.transform.Find("BulletGenPoint").transform;
            GameObject NewBullet = Pooler.GetPooledObject();
            NewBullet.SetActive(true);
            NewBullet.transform.position = BulletGenPoint.position;
            Fire1 = 0f;
        }
        else if(player == PlayerTwo)
        {
            Transform BulletGenPoint = PlayerTwo.GetComponent<Stats>().CurrentModel.transform.Find("BulletGenPoint").transform;
            GameObject NewBullet = Pooler1.GetPooledObject();
            NewBullet.SetActive(true);
            NewBullet.transform.position = BulletGenPoint.position;
            Fire2 = 0f;
        }
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
		
	}

	public override void LeftHeldAction(GameObject player)
	{
		player.transform.Translate(-0.1f, 0f, 0f);
	}

	public override void CenterHeldAction(GameObject player)
	{
		//player.transform.Translate(0f, -0.5f, 0f);

	}

	public override void RightHeldAction(GameObject player)
	{
        player.transform.Translate(0.1f, 0f, 0f);

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

	public void MyGameEnd ()
	{
        gameStart = false;
        GameManager.Instance.GameState = States.Results;

        if (GameManager.Instance.Players[2].Value.MiniGameScore < GameManager.Instance.Players[1].Value.MiniGameScore)
        {
            winner.text = "Player 1 Wins!";
            winner.gameObject.SetActive(true);

        }

        else if (GameManager.Instance.Players[2].Value.MiniGameScore > GameManager.Instance.Players[1].Value.MiniGameScore)
        {
            winner.text = "Player 2 Wins!";
            winner.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.Players[2].Value.MiniGameScore == GameManager.Instance.Players[1].Value.MiniGameScore)
        {
            winner.text = "DRAW!";
            winner.gameObject.SetActive(true);
        }

        PlayerOne.transform.localScale = new Vector3(1f, 1f, 1f);
        PlayerTwo.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
