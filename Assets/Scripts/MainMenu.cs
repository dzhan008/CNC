/*
Author: David Zhang
Description: Handles functionality for main menu
Requirements: The Main Menu Canvas

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    Button Play;
    [SerializeField]
    Button Settings;
    [SerializeField]
    Button Exit;
	// Use this for initialization
	void Start () {
        Debug.Log("Current State: " + GameManager.Instance.GameState);

    }
	
    public void onPlay()
    {
        GameManager.Instance.GameState = States.InGame;
        GameManager.Instance.LoadMiniGame();
        Debug.Log("Current State: " + GameManager.Instance.GameState);
        this.gameObject.SetActive(false);
    }

    public void onSettings()
    {
        Debug.Log("In settings!");
    }
    
    public void onExit()
    {
        Application.Quit();
    }
}
