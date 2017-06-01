using UnityEngine;
using System.Collections;

public class BookStackingScript : MonoBehaviour
{

    //book x: 1.33094
    //book y: 0.3164554
    public bool touched = false;

    public StackOverflowedMinigame stackOverflowedMinigame;
    public int scoreValue = 1;
    private int player_id;

    private Stats PlayerOneStats;
    private Stats PlayerTwoStats;

    void Start()
    {
        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;
    }

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnTriggerEnter2D(Collider2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.gameObject.transform.parent != null && c.gameObject.tag != "BookHolder" )
        {

            //then check the tag of the parent to see if the book is at the top of the stack
            //have to check for the book tag because if not then it will accept book stacked books and book which will cause a double collision

            if (c.gameObject.transform.parent.tag == "BookHolder" && this.gameObject.transform.tag == "Book" && c.gameObject.GetComponent<BookStackingScript>().touched != true)
            {
                ++c.gameObject.transform.parent.parent.GetComponent<PlayerScript>().BooksCarried; //see if this works
                /*
                //check for the appropiate player and increment the scoring 
                if (c.gameObject.transform.parent.GetComponent<Stats>().Id == 1)
                {
                    
                    //stackOverflowedMinigame.UpdateScore(++PlayerOneStats.MiniGameScore, 1);
                }
                else
                {
                    //stackOverflowedMinigame.UpdateScore(++PlayerTwoStats.MiniGameScore, 2);
                }
                */
                //if it is a player than put it with the player and stack it on top of the player
                c.gameObject.GetComponent<BookStackingScript>().touched = true;

                //turn off collider so that there is no way it could collide twice
                c.enabled = false;

                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                //assign the base book's parent to this
                this.transform.parent = c.transform.parent;

                //reassign layer to book touched so that other books don't collide with it
                c.gameObject.layer = LayerMask.NameToLayer("BookTouchedLayer");

                //place book on top of the other book
                this.transform.position = new Vector3(c.transform.position.x, c.transform.position.y + this.gameObject.GetComponent<Renderer>().bounds.size.y, 0);

                this.transform.tag = "BookTop";
                c.gameObject.transform.tag = "BookTouched";

            }
        }

        //if the tag is book then change the tag to book touched 
        else if (c.gameObject.tag == "BookHolder" && c.gameObject.transform.parent.tag == "Player")
        {
            ++c.gameObject.transform.parent.GetComponent<PlayerScript>().BooksCarried; //see if this works

            //change the book stacking script to true
            this.gameObject.GetComponent<BookStackingScript>().touched = false;

            this.gameObject.tag = "BookTop";
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            //set the position of the book to the current object's position also reposition the object NO LONGER NEED TO BCUZ OF 1 to 1 SCALING BUT KEPT HERE FOR REFERENCE
            /*
            Vector3 newScale = new Vector3(c.transform.localScale.x / this.transform.localScale.x,
                                                 c.transform.localScale.y / (this.transform.localScale.y),
                                                 0);
            */
            this.transform.parent = c.transform;
            //c.transform.localScale = newScale;


            this.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, 0);
        }
    }
}
