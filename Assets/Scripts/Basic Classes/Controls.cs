/*
Created By: David Zhang
Description: Universal class that defines the behavior of button presses for both players.
Requirements: A player GameObject.
*/

using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
    Key UpKey;
    Key LeftKey;
    Key CenterKey;
    Key RightKey;


    string UpButton;
    string LeftButton;
    string CenterButton;
    string RightButton;

	// Use this for initialization
	void Awake ()
    {
        int id = this.GetComponent<Stats>().Id;

        UpButton = "Up Button " + id;
        LeftButton = "Left Button " + id;
        CenterButton = "Center Button " + id;
        RightButton = "Right Button " + id;

        UpKey = new Key(UpButton, this.gameObject);
        LeftKey = new Key(LeftButton, this.gameObject);
        CenterKey = new Key(CenterButton, this.gameObject);
        RightKey = new Key(RightButton, this.gameObject);
	}
	
    //TO DO: Handle single tap key presses.
    /// <summary>
    /// Checks if the player is pressing the key and holding it and calls the function
    /// for the particular action. Only supports one key press and hold at a time.
    /// </summary>
	void Update ()
    {
        UpKey.UpdateKeys();
        LeftKey.UpdateKeys();
        CenterKey.UpdateKeys();
        RightKey.UpdateKeys();

    }

    /// <summary>
    /// Sets the delegates of each control schema to a particular function. Used to change the logic
    /// of each key tap press in the game.
    /// </summary>
    /// <param name="action_left"></param>
    /// <param name="action_center"></param>
    /// <param name="action_right"></param>
    public void SetTapControls(Key.TapA action_up, Key.TapA action_left, Key.TapA action_center, Key.TapA action_right)
    {
        UpKey.SetTapControl(action_up);
        LeftKey.SetTapControl(action_left);
        CenterKey.SetTapControl(action_center);
        RightKey.SetTapControl(action_right);

    }

    /// <summary>
    /// Sets the delegates of each control schema to a particular function. Used to change the logic
    /// of each key held press in the game.
    /// </summary>
    /// <param name="action_left"></param>
    /// <param name="action_center"></param>
    /// <param name="action_right"></param>
    public void SetHeldControls(Key.HeldA action_up, Key.HeldA action_left, Key.HeldA action_center, Key.HeldA action_right)
    {
        UpKey.SetHeldControl(action_up);
        LeftKey.SetHeldControl(action_left);
        CenterKey.SetHeldControl(action_center);
        RightKey.SetHeldControl(action_right);
        
    }

    /// <summary>
    /// Sets the delegates of each control schema to a particular function. Used to change the logic
    /// of each key release in the game. 
    /// </summary>
    /// <param name="action_up"></param>
    /// <param name="action_left"></param>
    /// <param name="action_center"></param>
    /// <param name="action_right"></param>
    public void SetReleaseControls(Key.RelA action_up, Key.RelA action_left, Key.RelA action_center, Key.RelA action_right)
    {
        UpKey.SetReleaseControl(action_up);
        LeftKey.SetReleaseControl(action_left);
        CenterKey.SetReleaseControl(action_center);
        RightKey.SetReleaseControl(action_right);
    }

    //Probably put some menu functions here?
}
