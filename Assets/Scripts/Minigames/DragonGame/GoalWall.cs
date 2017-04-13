using UnityEngine;
using System.Collections;

public class GoalWall : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if the right player hit the right goal
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Goal Wall!!! Game Over");
            other.GetComponent<Stats>().MiniGameScore++;
            GameObject game = GameObject.Find("HHD(Clone)");
            game.GetComponent<DragonMiniGame>().IsGameEnd = true;
            int winner = other.GetComponent<Stats>().Id;
            game.GetComponent<DragonMiniGame>().Winner = winner;
            string text = "Player " + winner + " wins!";
            game.GetComponent<DragonMiniGame>().GameOverText.text = text;
        }
    }
}


