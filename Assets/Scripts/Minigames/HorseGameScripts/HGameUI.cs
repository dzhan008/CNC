using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HGameUI : MonoBehaviour {

    // Use this for initialization

    public Text Countdown;
    public HorseControls GameManager;
    public GameObject[] Canvas;
	void Start () {
	    
	}
    
	// Update is called once per frame
	void Update () {
        float x = GameManager.Countdown;
        bool GameState = GameManager.RaceEnd;
        if (x >= 0)
        {
            Countdown.text = Mathf.Round(x).ToString();
        }
        else if ((x <= 0) && (GameState == false))
        {
            Debug.Log("Race has not ended");
            Countdown.text = "GO!";
            StartCoroutine(WaitAndSetActive(1f, 0, false));
        }
        else 
        {
            Debug.Log("Race is over");
            int y = Canvas[1].GetComponent<FinishLine>().WinningPlayer;
            Canvas[2].GetComponent<Text>().text = "Player " + y + " Wins!";
            StartCoroutine( WaitAndSetActive(1f, 2, true));
        }

    }
    private IEnumerator WaitAndSetActive(float waitTime, int UiElement, bool Display)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Canvas[UiElement].SetActive(Display);
        }
    }
}
