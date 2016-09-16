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
            Debug.Log("Death Wall!!!");
        }
        else
        {
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
}

