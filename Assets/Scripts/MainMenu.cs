﻿/*
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
    GameObject Menu;
    [SerializeField]
    GameObject Settings;

	// Use this for initialization
	void Start () {
        Debug.Log("Current State: " + GameManager.Instance.GameState);
    }
	
    public void onPlay()
    {
        StartCoroutine(Play(2));
    }

    public void onSettings()
    {
        Menu.SetActive(false);
        Settings.SetActive(true);
    }

    public void onBack()
    {
        Settings.SetActive(false);
        Menu.SetActive(true);
    }
    
    public void onExit()
    {
        Application.Quit();
    }

    IEnumerator Play(float time)
    {
        UIManager.Instance.FadeIn();
        yield return new WaitForSeconds(time);
        UIManager.Instance.FadeOut();
        GameManager.Instance.LoadMiniGame();
        Debug.Log("Current State: " + GameManager.Instance.GameState);
        this.gameObject.SetActive(false);
    }


}
