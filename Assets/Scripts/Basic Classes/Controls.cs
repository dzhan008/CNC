/*
Created By: David Zhang
Description: Universal class that defines the behavior of button presses for both players.
Requirements: A player GameObject.
*/

using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{

    public int Id;

    public delegate void leftA(GameObject player);
    public delegate void centerA(GameObject player);
    public delegate void rightA(GameObject player);

    leftA LeftAction;
    centerA CenterAction;
    rightA RightAction;

    private bool LeftButtonDown;
    private bool LeftButtonHeld;
    private bool LeftButtonUp;

    private bool CenterButtonDown;
    private bool CenterButtonHeld;
    private bool CenterButtonUp;

    private bool RightButtonDown;
    private bool RightButtonHeld;
    private bool RightButtonUp;

    string LeftButton;
    string CenterButton;
    string RightButton;

	// Use this for initialization
	void Start ()
    {
        LeftButton = "Left Button " + Id;
        CenterButton = "Center Button " + Id;
        RightButton = "Right Button " + Id;
	}
	
    //TO DO: Handle single tap key presses.
    /// <summary>
    /// Checks if the player is pressing the key and holding it and calls the function
    /// for the particular action. Only supports one key press and hold at a time.
    /// </summary>
	void Update ()
    {

        LeftButtonDown = Input.GetButtonDown(LeftButton);
        LeftButtonHeld = Input.GetButton(LeftButton);
        LeftButtonUp = Input.GetButtonDown(LeftButton);

        CenterButtonDown = Input.GetButtonDown(CenterButton);
        CenterButtonHeld = Input.GetButton(CenterButton);
        CenterButtonUp = Input.GetButtonUp(CenterButton);

        RightButtonDown = Input.GetButtonDown(RightButton);
        RightButtonHeld = Input.GetButton(RightButton);
        RightButtonUp = Input.GetButtonUp(RightButton);


        if (LeftButtonHeld)
        {
            LeftAction(this.gameObject);
        }
        else if (RightButtonHeld)
        {
            RightAction(this.gameObject);
        }
        else if (CenterButtonHeld)
        {
            CenterAction(this.gameObject);
        }

    }

    /// <summary>
    /// Sets the delegates of each control schema to a particular function. Used to change the logic
    /// of each key press in the game.
    /// </summary>
    /// <param name="action_left"></param>
    /// <param name="action_center"></param>
    /// <param name="action_right"></param>
    public void SetControls(leftA action_left, centerA action_center, rightA action_right)
    {
        LeftAction = action_left;
        CenterAction = action_center;
        RightAction = action_right;
    }

    //Probably put some menu functions here?
}
