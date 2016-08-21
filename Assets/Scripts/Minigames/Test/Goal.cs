using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public int id;
    public Text scoreText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {   
        //Checks if the right player hit the right goal
        if(other.gameObject.tag == "Player" && id == other.GetComponent<Stats>().id)
        {
            
            other.gameObject.GetComponent<Stats>().incrementMiniGameScore(1);
            scoreText.text = "Score: " + other.gameObject.GetComponent<Stats>().miniGameScore;
            resetPosition();
        }
        else
        {
            Debug.Log("No hit!");
        }
    }

    void resetPosition()
    {
        GameManager.Instance.playerOne.transform.position = new Vector3(-30f, 5f, 0f);
        GameManager.Instance.playerTwo.transform.position = new Vector3(-20f, 5f, 0f);
    }

}
