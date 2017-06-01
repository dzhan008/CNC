using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HuntingMinigame : Minigame {

	private GameObject Player1;
	private GameObject Player2;
	private Stats Player1Stats;
	private Stats Player2Stats;
	private GameObject Generator;
    private GameObject Generator1;
    private HuntingObjectPooler Pooler;
    private HuntingObjectPooler Pooler1;
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
		Player1 = GameManager.Instance.Players [1].Key;
		Player2 = GameManager.Instance.Players [2].Key;
		Player1Stats = GameManager.Instance.Players [1].Value;
		Player2Stats = GameManager.Instance.Players [2].Value;
		SetControls (Player1);
		SetControls (Player2);
        Fire1 = 1f - ((Player1.GetComponent<Stats>().Dex) * .05f);
        Fire2 = 1f - ((Player2.GetComponent<Stats>().Dex) * .05f);
        Fire1Max = 1f - ((Player1.GetComponent<Stats>().Dex) * .05f);
        Fire2Max = 1f - ((Player2.GetComponent<Stats>().Dex) * .05f);
        Bull.GetComponent<Bullet>().speed = 1f - ((Player1.GetComponent<Stats>().Intel) * .1f);
        Bull1.GetComponent<Bullet1>().speed = 1f - ((Player2.GetComponent<Stats>().Intel) * .1f);
        if (Player1.GetComponent<Stats>().Str >= 1 || Player1.GetComponent<Stats>().Str <= 3)
        {
            Bull.GetComponent<Bullet>().damage = 100;
        }

        else if (Player1.GetComponent<Stats>().Str > 3 || Player1.GetComponent<Stats>().Str <= 6)
        {
            Bull.GetComponent<Bullet>().damage = 125;
        }

        else if (Player1.GetComponent<Stats>().Str > 6 || Player1.GetComponent<Stats>().Str <= 9)
        {
            Bull.GetComponent<Bullet>().damage = 167;
        }

        else if (Player1.GetComponent<Stats>().Str >= 10)
        {
            Bull.GetComponent<Bullet>().damage = 250;
        }

        if (Player2.GetComponent<Stats>().Str >= 1 || Player2.GetComponent<Stats>().Str <= 3)
        {
            Bull1.GetComponent<Bullet1>().damage = 100;
        }

        else if (Player2.GetComponent<Stats>().Str > 3 || Player2.GetComponent<Stats>().Str <= 6)
        {
            Bull1.GetComponent<Bullet1>().damage = 125;
        }

        else if (Player2.GetComponent<Stats>().Str > 6 || Player2.GetComponent<Stats>().Str <= 9)
        {
            Bull1.GetComponent<Bullet1>().damage = 167;
        }

        else if (Player2.GetComponent<Stats>().Str >= 10)
        {
            Bull1.GetComponent<Bullet1>().damage = 250;
        }

        //TODO: Move this to the instructions screen!
    }

    // Update is called once per frame
    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(5);
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
                MyCoroutine();
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
        Generator = GameObject.Find("BulletGenerator");
        Generator1 = GameObject.Find("BulletGenerator (1)");
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
		Transform BulletGenePoint = player.transform.Find ("BulletGenPoint").transform;
        if(player == Player1 && Fire1 > Fire1Max)
        {
            //Debug.Log("pkcfdk");
            GameObject NewBullet = Pooler.GetPooledObject();
            NewBullet.SetActive(true);
            NewBullet.transform.position = BulletGenePoint.position;
            Fire1 = 0f;
        }
        if (player == Player2 && Fire2 > Fire2Max)
        {
            //Debug.Log("player2");
            GameObject NewBullet = Pooler1.GetPooledObject();
            Debug.Log(NewBullet);
            NewBullet.SetActive(true);
            NewBullet.transform.position = BulletGenePoint.position;
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
        foreach (GameObject monObj in GameObject.FindGameObjectsWithTag("Goblin"))
        {
            monObj.SetActive(false);
        }
    }
}
