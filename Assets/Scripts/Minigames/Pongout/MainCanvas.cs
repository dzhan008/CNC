using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
    public void EditText(int PlayerID)
    {
        GetComponentsInChildren<Text>()[PlayerID - 1].text = "Player " + PlayerID + " " + GameManager.Instance.Players[PlayerID].Value.MiniGameScore;
    }
    public void WinMsg()
    {
        if (GameManager.Instance.Players[1].Value.MiniGameScore > GameManager.Instance.Players[2].Value.MiniGameScore)
        {
            GetComponentsInChildren<Text>()[2].text = "Player 1 Wins";
        }
        else if (GameManager.Instance.Players[1].Value.MiniGameScore < GameManager.Instance.Players[2].Value.MiniGameScore)
        {
            GetComponentsInChildren<Text>()[2].text = "Player 2 Wins";
        }
        else { GetComponentsInChildren<Text>()[2].text = "Draw!"; }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
