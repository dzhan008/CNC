using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	private GameObject DestructPoint;
	public float speed;
    public int damage = 50;
	// Use this for initialization
	void Start () 
	{
		DestructPoint = GameObject.Find ("BulletDestruct");
	}


	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.transform.position.y > DestructPoint.transform.position.y) 
		{
			gameObject.SetActive (false);
		} 

		else 
		{
			gameObject.transform.Translate (0, speed, 0);
		}

	}
}
