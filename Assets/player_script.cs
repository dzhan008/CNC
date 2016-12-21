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
            c.collider.gameObject.tag = "BookTouched";
            c.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            c.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //Debug.Log("c.localscale: " + c.transform.localScale.x + " this.transform: " + this.transform.localScale.x + " = " + c.transform.localScale.x / this.transform.localScale.x);

            //set the position of the book to the current object's position also reposition the object
            Vector3 newScale = new Vector3(c.transform.localScale.x / this.transform.localScale.x,
                                                 c.transform.localScale.y / (this.transform.localScale.y),
                                                 0);
            c.transform.parent = this.transform;
            c.transform.localScale = newScale;

            c.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (this.transform.position.y/8), 0);
        }
    }
}
