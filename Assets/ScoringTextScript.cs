using UnityEngine;
using System.Collections;

public class ScoringTextScript : MonoBehaviour {

    

    TextAlignment 

	// Use this for initialization
	void Start () {
	    UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateText();
    }

    void UpdateText() {
        scoreText.text = "Score: " +  score;
    }
}
