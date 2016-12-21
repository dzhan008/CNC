using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class BookDeSpawn : StackOverflowedMinigame
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Book" || col.gameObject.tag == "BookTouched")
        {
            col.gameObject.transform.rotation = Quaternion.identity;
            col.gameObject.SetActive(false);

            //reset the book tag so that rescalability could be applied to it again
            col.collider.gameObject.tag = "Book";
        }
    }
}
