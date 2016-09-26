using UnityEngine;
using System.Collections;

public class HuntingMinigame : Minigame {

	private GameObject Player1;
	private GameObject Player2;
	private Stats Player1Stats;
	private Stats Player2Stats;
	private GameObject Generator;
	private ObjectPooler Pooler;
	// Use this for initialization
	void Start () 
	{
		Player1 = GameManager.Instance.Players [1].Key;
		Player2 = GameManager.Instance.Players [2].Key;
		Player1Stats = GameManager.Instance.Players [1].Value;
		Player2Stats = GameManager.Instance.Players [2].Value;
		SetControls (Player1);
		SetControls (Player2);
		Generator = GameObject.Find ("BulletGenerator");
		Pooler = Generator.GetComponent <ObjectPooler> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void UpTapAction(GameObject player)
	{
		Transform BulletGenePoint = player.transform.Find ("BulletGenPoint").transform;
		GameObject NewBullet = Pooler.GetPooledObject ();
		NewBullet.SetActive (true);
		NewBullet.transform.position = BulletGenePoint.position;
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
		player.transform.Translate(-0.5f, 0f, 0f);
	}

	public override void CenterHeldAction(GameObject player)
	{
		player.transform.Translate(0f, -0.5f, 0f);
	}

	public override void RightHeldAction(GameObject player)
	{
		player.transform.Translate(0.5f, 0f, 0f);
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

	public override void GameEnd ()
	{
		
	}
}
