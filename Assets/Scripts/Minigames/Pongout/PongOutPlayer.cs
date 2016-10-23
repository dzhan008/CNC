using UnityEngine;
using System.Collections;

public class PongOutPlayer : MonoBehaviour {
    public string Tag = "Ball";
    public GameObject holding = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator ResetVelocity(GameObject other, float time)
    {

        holding = other;
        Vector2 Zero = new Vector2(0, 0);
        Vector2 Entry = other.GetComponent<Rigidbody2D>().velocity;
        other.GetComponent<Rigidbody2D>().velocity = Zero;
        //if (Hold == true)
        //{
        //  yield return null;
        // }
        other.transform.parent = this.transform;
        yield return new WaitForSeconds(time);
        
        other.transform.parent = null; 
        other.GetComponent<Rigidbody2D>().velocity = Entry;
        holding = null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Colliding With Paddle");
        if(col.gameObject.tag == Tag )
        {
            float StoppedTime = this.GetComponent<Stats>().Intel /10;
            StartCoroutine(ResetVelocity(col.gameObject, StoppedTime));
        }
    }
}
