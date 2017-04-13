using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    /*
    This might be used to handle general UI that will reoccur during gameplay OR be used once such as:
    Fading Screens
    Character Selection(?)
    Progress (After every minigame)
    End of Game Screen
    */
    [SerializeField]
    private GameObject MainMenuCanvas;
    [SerializeField]
    private Image FadeScreen;
    [SerializeField]
    private GameObject ProgressScreen;
    [SerializeField]
    private GameObject MiniGameScreen;
    [SerializeField]
    private Text PlayerOneScore;
    [SerializeField]
    private Text PlayerTwoScore;
    [SerializeField]
    private GameObject PlayerOneModel;
    [SerializeField]
    private GameObject PlayerTwoModel;

    public float FadeTime
    {
        get; set; }

    // Use this for initialization
    void Start ()
    {
        FadeTime = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowProgressScreen()
    {
        PlayerOneScore.text = GameManager.Instance.Players[1].Value.Score.ToString();
        PlayerTwoScore.text = GameManager.Instance.Players[2].Value.Score.ToString();
        ProgressScreen.SetActive(true);
    }

    public void DisableProgressScreen()
    {
        ProgressScreen.SetActive(false);
    }

    public void OnContinue()
    {
        GameManager.Instance.QueueNewGame();
    }


    public void FadeIn()
    {
        FadeScreen.gameObject.SetActive(true);
        FadeScreen.canvasRenderer.SetAlpha(0.0f);
        FadeScreen.CrossFadeAlpha(1, FadeTime, false);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(FadeTime));
    }

    IEnumerator FadeOut(float time)
    {
        FadeScreen.CrossFadeAlpha(0, time, false);
        yield return new WaitForSeconds(time);
        FadeScreen.gameObject.SetActive(false);
    }

    /* Demo UI Functions Here */
    public void ShowMiniGameScreen()
    {
        MiniGameScreen.SetActive(true);
    }

    public void OnSelectGame(string name)
    {

        StartCoroutine(SelectGame(2, name));
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        //TO DO: Find a way to reset the game without having to remake the scene?
        /*MainMenuCanvas.SetActive(true);
        MiniGameScreen.SetActive(false);
        PlayerOneModel.SetActive(true);
        PlayerTwoModel.SetActive(true);
        AudioManager.Instance.SetSong("Title");*/
    }

    IEnumerator SelectGame(float time, string name)
    {
        UIManager.Instance.FadeIn();
        yield return new WaitForSeconds(time);
        UIManager.Instance.FadeOut();
        GameManager.Instance.LoadMiniGame(name);
        Debug.Log("Current State: " + GameManager.Instance.GameState);
        MiniGameScreen.SetActive(false);
    }


}
