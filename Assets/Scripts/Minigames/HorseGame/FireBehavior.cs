﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{

    public GameObject Target; //reference to target player
    Vector2 Direction;
    Rigidbody2D rb;
    public float TrackingDistance; //Distance to stop tracking at scales on INT
    void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        Direction = Target.transform.position - transform.position;

    }
    void LookAt2D() //Looks at target transform in a 2D plane
    {
        var dir = Target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    // Update is called once per frame
    Vector3 dir;
    public float lifetime;
    void Update () {
        Direction = Target.transform.position - transform.position;
        
        if (Direction.magnitude >= TrackingDistance)
        {
            dir = Target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else
        {

        }
        rb.AddForce(dir);
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        lifetime -= Time.deltaTime;
        //Debug.Log(Direction.magnitude);
	}
    void OnCollisonEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            Destroy(this.gameObject);
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            Destroy(this.gameObject);
        }

    }
}
