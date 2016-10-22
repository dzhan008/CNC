using UnityEngine;
using System.Collections;

public class ScorePointTrigger : MonoBehaviour {
    [SerializeField]
    int ScoreForThisPlayer = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            GameManager.Instance.Players[ScoreForThisPlayer].Value.MiniGameScore++;
        }
    }
}
