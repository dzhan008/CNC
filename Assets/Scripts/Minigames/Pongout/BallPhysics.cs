/*
Created By: Rica Feng
Description: Controls ball physics
Requirements: None
*/


using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour
{
    [SerializeField]
    public int BallId = 1;

    [SerializeField]
    private float BallSpeed = 10f;

    [SerializeField]
    private Vector2 StartDirection;

    public Rigidbody2D RB;

	// Use this for initialization
	void Start ()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.AddForce(StartDirection, ForceMode2D.Force);
	}
	
	// Update is called once per frame
	void Update ()
    {
        RB.velocity = BallSpeed * (RB.velocity.normalized);
    }
}
