using UnityEngine;
using System.Collections;

public class Artifact : MonoBehaviour {

    float Health = 50;
    public int ArtifactId;
    float BaseDamage = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
	
	}
    void ApplyDamage(float Damage)
    {
        int s = GameManager.Instance.Players[ArtifactId].Value.Str;
        Health -= Damage * s;
        Debug.Log("str: " + s);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ball")
        {
            Debug.Log("ball id: " + col.gameObject.GetComponent<BallPhysics>().BallId + " artifact id: " + ArtifactId);
            if(col.gameObject.GetComponent<BallPhysics>().BallId  == ArtifactId )
            {
                ApplyDamage(BaseDamage);
            }
        }
    }
}
