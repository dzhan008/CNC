using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private GameObject DestructPoint;
	public float WalkSpeed;
	// Use this for initialization
	void Start () 
	{
		DestructPoint = GameObject.Find ("MonsterPass");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.transform.position.y < DestructPoint.transform.position.y) 
		{
			gameObject.SetActive (false);
		} 

		else 
		{
			gameObject.transform.Translate (0, WalkSpeed, 0);
		}

	}
}
