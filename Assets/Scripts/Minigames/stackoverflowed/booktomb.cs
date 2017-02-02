using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerEnter2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.root.tag != null)
        {
            if (c.gameObject.transform.root.tag == "Player")
            {
                Debug.Log("I'm in");
                c.gameObject.transform.root.GetComponent<player_script>().isOnTomb = true;
            }
        }
    }

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerExit2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.root.tag != null)
        {
            if (c.gameObject.transform.root.tag == "Player")
            {
                Debug.Log("I'm out");
                c.gameObject.transform.root.GetComponent<player_script>().isOnTomb = false;
            }
        }
    }
}
