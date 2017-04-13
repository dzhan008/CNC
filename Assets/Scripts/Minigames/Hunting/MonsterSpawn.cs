using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

	private GameObject MSpawn1;
	private GameObject MSpawn2;
	private GameObject Generator;
	private HuntingObjectPooler Pooler;
	public float CountDown;

	// Use this for initialization
	void Start () 
	{
		CountDown = 3f;
		MSpawn1 = GameObject.Find ("MSpawn1");
		MSpawn2 = GameObject.Find ("MSpawn2");
		Generator = GameObject.Find ("MonsterGenerator");
		Pooler = Generator.GetComponent <HuntingObjectPooler> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CountDown -= Time.deltaTime;
		if (CountDown < 0) 
		{
			GameObject NewMonster = Pooler.GetPooledObject ();
			Vector3 NewMonsterPosition = NewMonster.transform.position;
			NewMonster.SetActive (true);
			NewMonster.transform.position = new Vector3 (Random.Range (MSpawn1.transform.position.x, MSpawn2.transform.position.x), MSpawn1.transform.position.y, NewMonsterPosition.z);
			CountDown = 3f;
		}
			
	}

}
