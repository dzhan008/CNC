using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ground : MonoBehaviour {
	
	public int Id;
	public bool isColliding;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
/*
	void OnCollisionEnter(Collision2D other)
	{
		if(other.gameObject.tag.Equals("Player") && Id == other.GetComponent<Stats>().Id) {
			isColliding = true;
		}
	}
*/
	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.tag.Equals ("Player") && Id == other.gameObject.GetComponent<Stats>().Id) {	
			isColliding = true;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag.Equals("Player") && Id == other.gameObject.GetComponent<Stats>().Id)
			isColliding = false;

	}
}
