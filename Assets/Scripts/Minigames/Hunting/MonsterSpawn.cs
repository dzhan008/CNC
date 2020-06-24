using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

	private GameObject Generator;
	private HuntingObjectPooler Pooler;
	public float CountDown;
    public HuntingMinigame huntG;
	public GameObject MSpawn1;
	public GameObject MSpawn2;

	// Use this for initialization
	void Start () 
	{
		CountDown = 3f;
		Generator = GameObject.Find ("MonsterGenerator");
		Pooler = this.GetComponent <HuntingObjectPooler> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CountDown -= Time.deltaTime;
		if (huntG.gameStart == true && CountDown < 0) 
		{
			GameObject NewMonster = Pooler.GetPooledObject ();
			Vector3 NewMonsterPosition = NewMonster.transform.position;
			NewMonster.SetActive (true);
			NewMonster.transform.position = new Vector3 (Random.Range (MSpawn1.transform.position.x, MSpawn2.transform.position.x), MSpawn1.transform.position.y, NewMonsterPosition.z);
			CountDown = Random.Range(2f, 3f);
		}
			
	}

}
