using UnityEngine;
using System.Collections;

public class DeathWall : MonoBehaviour
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
            Debug.Log("Death Wall!!! Game Over");
            GameObject game = GameObject.Find("HHD(Clone)");
            game.GetComponent<DragonMiniGame>().IsGameEnd = true;
            int player_id = other.GetComponent<Stats>().Id;
            int winner = 2;
            if (player_id == 2) winner = 1;
            game.GetComponent<DragonMiniGame>().Winner = winner;
            string text = "Player " + winner + " wins!";
            game.GetComponent<DragonMiniGame>().GameOverText.text = text;
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.SetActive(false);
        }
    }
}


