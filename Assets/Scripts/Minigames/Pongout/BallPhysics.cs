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
    GameObject MainCanvas;
    // Use this for initialization
    void Start ()
    {
        MainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        RB = GetComponent<Rigidbody2D>();
        RB.AddForce(StartDirection, ForceMode2D.Force);
	}
	
	// Update is called once per frame
	void Update ()
    {
        RB.velocity = BallSpeed * (RB.velocity.normalized);
    }
    private IEnumerator ResetBall()
    {
        RB.velocity = new Vector2(0,0);
        this.transform.position = new Vector2(Random.Range(-1f,1f), 0);

        //yield return new WaitForSeconds(2);
        StartDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        RB.AddForce(StartDirection, ForceMode2D.Force);
        yield return null;

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Respawn")
        {
            if(col.gameObject.name == "1")
            {
                GameManager.Instance.Players[1].Value.MiniGameScore -= 10;
                MainCanvas.GetComponent<MainCanvas>().EditText(1);
            }
            if(col.gameObject.name == "2")
            {
                GameManager.Instance.Players[2].Value.MiniGameScore -= 10;
                MainCanvas.GetComponent<MainCanvas>().EditText(2);
            }
            StartCoroutine(ResetBall());
            
        }
    }
}
