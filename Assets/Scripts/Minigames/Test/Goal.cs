/*
Created By: David Zhang
Description: Defines the logic for each goal post in the minigame.
Requirements: A goal post GameObject
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public int Id;
    public Text ScoreText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {   
        //Checks if the right player hit the right goal
        if(other.gameObject.tag == "Player" && Id == other.GetComponent<Stats>().Id)
        {
            
            other.gameObject.GetComponent<Stats>().IncrementMiniGameScore(1);
            ScoreText.text = "Score: " + other.gameObject.GetComponent<Stats>().MiniGameScore;
            ResetPosition();
        }
        else
        {
            Debug.Log("No hit!");
        }
    }

    /// <summary>
    /// Helper function that resets each player's position if a player makes a point.
    /// </summary>
    void ResetPosition()
    {
        GameManager.Instance.Players[1].Key.transform.position = new Vector3(-30f, 5f, 0f);
        GameManager.Instance.Players[2].Key.transform.position = new Vector3(-20f, 5f, 0f);
    }

}
