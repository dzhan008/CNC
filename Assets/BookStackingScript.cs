using UnityEngine;
using System.Collections;

public class BookStackingScript : MonoBehaviour {

    //book x: 1.33094
    //book y: 0.3164554
    public bool touched = false;

    public int book_id = 1;

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnCollisionEnter2D(Collision2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.collider.gameObject.transform.parent != null)
        {
            //then check the tag of the parent to see if the book is at the top of the stack
            if (c.gameObject.transform.parent.tag == "Player" && c.collider.gameObject.transform.tag == "BookTop" && c.gameObject.GetComponent<BookStackingScript>().touched != true)
            {
                //if it is a player than put it with the player and stack it on top of the player
                c.gameObject.GetComponent<BookStackingScript>().touched = true;

                //turn off collider so that there is no way it could collide twice
                c.collider.enabled = false;

                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                //assign the base book's parent to this
                this.transform.parent = c.transform.parent;

                //reassign layer to book touched so that other books don't collide with it
                c.gameObject.layer = LayerMask.NameToLayer("BookTouchedLayer");

                Debug.Log("ID: " + this.book_id + " Before setting x: " + c.transform.position.x + " y.trans.postiion: " + c.transform.position.y + " this.bound.y: " + this.gameObject.GetComponent<Renderer>().bounds.size.y);

                //place book on top of the other book
                this.transform.position = new Vector3(c.transform.position.x, c.transform.position.y + this.gameObject.GetComponent<Renderer>().bounds.size.y, 0);

                this.transform.tag = "BookTop";
                c.gameObject.transform.tag = "BookTouched";
                
            }
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        if (collider.name == "target")
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;

            bool right = contactPoint.x > center.x;
            bool top = contactPoint.y > center.y;
        }
    }
    */
}
