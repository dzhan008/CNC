using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour
{

    private int player_id;
    private bool isOnTomb = false;

    void Start()
    {
        //set player id during the beginnning
        player_id = this.gameObject.GetComponent<Stats>().Id;
    }

    //update books carried from book stacking script when a book is caught
    private int _BooksCarried = 0;
    public int BooksCarried
    {
        get
        {
            return _BooksCarried;
        }
        set
        {
            _BooksCarried = value;
            Debug.Log("Player: " + player_id + " Has " + _BooksCarried + " Books");
        }
    }

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerEnter2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.tag != null)
        {
            if (c.gameObject.transform.tag == "BookTomb")
            {
                Debug.Log("I'm in");
                this.isOnTomb = true;
            }
        }
    }

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerExit2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.tag != null)
        {
            if (c.gameObject.transform.tag == "BookTomb")
            {
                Debug.Log("I'm out");
                this.isOnTomb = false;
            }
        }
    }
}
