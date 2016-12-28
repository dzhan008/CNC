using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("I'm hit!");
        Debug.Log("collide (tag) : " + c.collider.gameObject.tag);

        //if the tag is book then change the tag to book touched 
        if (c.collider.gameObject.tag == "Book")
        {
            //change the book stacking script to true
            c.gameObject.GetComponent<BookStackingScript>().touched = false;

            c.collider.gameObject.tag = "BookTop";
            c.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
