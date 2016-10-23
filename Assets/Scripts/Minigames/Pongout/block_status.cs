using UnityEngine;
using System.Collections;

public class block_status : MonoBehaviour {

	private int health = 100;
	public int id = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider_2d) {
		if (health <= 0) {
			gameObject.SetActive (false);
		}
	}

	void OnCollideEnter2D(Collider2D collider_2d) {
		if (collider_2d.gameObject.tag == "Ball" && collider_2d.gameObject.GetComponent<BallPhysics>().BallId == id) {
			health--;
		}
	}
}
