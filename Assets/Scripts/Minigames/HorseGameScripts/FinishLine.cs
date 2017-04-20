using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

    public string MinigameManager; //Name of the minigame manager
    public int WinningPlayer;
    private GameObject GM;
    private bool GameEnd;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find(MinigameManager);
        GameEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D Player)
    {
        int PlayerID = Player.gameObject.GetComponent<Stats>().Id;
        WinningPlayer = PlayerID;
        GM.GetComponent<HorseControls>().HGameEnd();
        Debug.Log("Player " + PlayerID + " wins!");
    }
}
