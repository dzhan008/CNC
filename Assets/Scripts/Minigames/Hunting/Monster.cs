using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monster : MonoBehaviour {

    public int health = 100;
	private GameObject DestructPoint;
	public float WalkSpeed;


	// Use this for initialization
	void Start () 
	{
		DestructPoint = GameObject.Find ("MonsterPass");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other.gameObject.name == "Bullet(Clone)")
            {
               health = health - other.gameObject.GetComponent<Bullet>().damage;
            }
            else if (other.gameObject.name == "Bullet 1(Clone)")
            {
                health = health - other.gameObject.GetComponent<Bullet1>().damage;
            }
            //Debug.Log(health);
            if (health <= 0)
            {  
				health = 100;
				GameObject Hunting = GameObject.Find ("HuntingMinigame");
				//Debug.Log (other.gameObject.name + "==" + "Bullet(Clone)");
				if (other.gameObject.name == "Bullet(Clone)") {
					Debug.Log ("score: " + GameManager.Instance.Players [1].Value.MiniGameScore);
					GameManager.Instance.Players [1].Value.MiniGameScore += 10;
					Hunting.GetComponent <HuntingMinigame> ().SetScoreP1 ();
					Debug.Log ("Kill by Player 1");
				}
				else if (other.gameObject.name == "Bullet 1(Clone)") {
					GameManager.Instance.Players [2].Value.MiniGameScore += 10;
					Hunting.GetComponent <HuntingMinigame> ().SetScoreP2 ();
					Debug.Log("Kill by Player 2");
				}
//				Debug.Log ("Hit");
				gameObject.SetActive(false); 
            }
            other.gameObject.SetActive(false);
        }

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


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {

        }
    }
}
