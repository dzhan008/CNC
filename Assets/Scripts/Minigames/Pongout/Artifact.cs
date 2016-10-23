using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Artifact : MonoBehaviour {

    float Health = 50;
    public int ArtifactId;
    float BaseDamage = 5f;

    GameObject MainCanvas;



	// Use this for initialization
	void Start () {
        MainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
	}
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
        {
            GameManager.Instance.Players[ArtifactId].Value.MiniGameScore += 10;
            MainCanvas.GetComponent<MainCanvas>().EditText(ArtifactId);
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
