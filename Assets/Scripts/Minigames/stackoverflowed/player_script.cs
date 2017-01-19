using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour
{

    public StackOverflowedMinigame stackOverflowedMinigame;
    public int scoreValue = 1;
    private int player_id;

    private Stats PlayerOneStats;
    private Stats PlayerTwoStats;

    void start()
    {
        PlayerOneStats = GameManager.Instance.Players[1].Value;
        PlayerTwoStats = GameManager.Instance.Players[2].Value;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("I'm hit!");
        Debug.Log("collide (tag) : " + c.gameObject.tag);

        //if the tag is book then change the tag to book touched 
        if (c.gameObject.tag == "Book")
        {
            //scoring
            //check for the appropiate player and increment the scoring
            if (this.GetComponent<Stats>().GetInstanceID() == 2)
            {
                scoreValue = PlayerOneStats.MiniGameScore++;
                Debug.Log("From Book Stacking: ScoreValue: " + scoreValue);
                stackOverflowedMinigame.AddScore(scoreValue, 1);
            }
            else
            {
                scoreValue = PlayerTwoStats.MiniGameScore++;
                Debug.Log("From Book Stacking: ScoreValue: " + scoreValue);
                stackOverflowedMinigame.AddScore(scoreValue, 2);
            }

            //change the book stacking script to true
            c.gameObject.GetComponent<BookStackingScript>().touched = false;

            c.gameObject.tag = "BookTop";
            c.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            c.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //Debug.Log("c.localscale: " + c.transform.localScale.x + " this.transform: " + this.transform.localScale.x + " = " + c.transform.localScale.x / this.transform.localScale.x);

            //set the position of the book to the current object's position also reposition the object NO LONGER NEED TO BCUZ OF 1 to 1 SCALING BUT KEPT HERE FOR REFERENCE
            /*
            Vector3 newScale = new Vector3(c.transform.localScale.x / this.transform.localScale.x,
                                                 c.transform.localScale.y / (this.transform.localScale.y),
                                                 0);
            */
            c.transform.parent = this.transform;
            //c.transform.localScale = newScale;

            //reassign this layer so that other books don't collide with it
            this.gameObject.layer = LayerMask.NameToLayer("BookTouchedLayer");

            c.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
    }
}
