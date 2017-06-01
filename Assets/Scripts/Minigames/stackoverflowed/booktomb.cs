using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booktomb : MonoBehaviour
{
    [SerializeField]
    private GameObject game;

    // Use this for initialization
    void Start()
    {

    }

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerEnter2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.tag != null && GameManager.Instance.GameState == States.InGame)
        {
            if (c.gameObject.transform.root.tag == "Player")
            {
                c.gameObject.transform.root.GetComponent<PlayerScript>().isOnTomb = true;
            }
        }
    }

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerExit2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.root.tag != null && GameManager.Instance.GameState == States.InGame)
        {

            if (c.gameObject.transform.root.tag == "Player")
            {
                c.gameObject.transform.root.GetComponent<PlayerScript>().isOnTomb = false;
            }
        }
    }
}
