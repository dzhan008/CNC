using UnityEngine;
using System.Collections;

public class BookStackingScript : MonoBehaviour {

    //book x: 1.33094
    //book y: 0.3164554

    //after the first book check to see if the following book belongs to the player if so then put it under the player book
    private void OnCollisionEnter2D(Collision2D c)
    {
        //to make sure that we don't get NULL exception error check to see that the parent is not null
        if (c.collider.gameObject.transform.parent != null)
        {
            //then check the tag of the parent
            if (c.collider.gameObject.transform.parent.tag == "Player" && c.collider.gameObject.transform.tag == "BookTouched")
            {
                //if it is a player than put it with the player and stack it on top of the player
                this.transform.tag = "BookTouched";
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //Debug.Log("c.localscale: " + c.transform.localScale.x + " this.transform: " + this.transform.localScale.x + " = " + c.transform.localScale.x / this.transform.localScale.x);

                //set the position of the book to the current object's position also reposition the object
                
                Vector3 newScale = new Vector3(this.transform.localScale.x / c.transform.parent.localScale.x,
                                                     this.transform.localScale.y / (c.transform.parent.localScale.y),
                                                     0);
                
                //assign the base book's parent to this
                this.transform.parent = c.transform.parent;
                this.transform.localScale = newScale;

                this.transform.position = new Vector3(c.transform.position.x, c.transform.position.y - (c.transform.position.y / 8), 0);
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
