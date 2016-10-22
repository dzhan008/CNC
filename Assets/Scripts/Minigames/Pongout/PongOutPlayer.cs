using UnityEngine;
using System.Collections;

public class PongOutPlayer : MonoBehaviour {
    public string Tag = "Ball";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator ResetVelocity(GameObject other, float time)
    {
        Vector2 Zero = new Vector2(0, 0);
        Vector2 Entry = other.GetComponent<Rigidbody2D>().velocity;
        other.GetComponent<Rigidbody2D>().velocity = Zero;
        //if (Hold == true)
        //{
          //  yield return null;
       // }
        yield return new WaitForSeconds(time);
        other.GetComponent<Rigidbody2D>().velocity = Entry;

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == Tag)
        {
            float StoppedTime = 0.01f;
            StartCoroutine(ResetVelocity(col.gameObject, StoppedTime));
        }
    }
}
