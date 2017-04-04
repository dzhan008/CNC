using UnityEngine;
using System.Collections;

public class Key
{
    GameObject player;

    public delegate void TapA(GameObject player);
    public delegate void HeldA(GameObject player);
    public delegate void RelA(GameObject player);

    TapA TapAction;
    HeldA HeldAction;
    RelA ReleaseAction;

    private string Button;

    private bool _ButtonDown;
    public bool ButtonDown
    {
        get { return _ButtonDown; }
        set { _ButtonDown = value; }
    }

    private bool _ButtonHeld;
    public bool ButtonHeld
    {
        get { return _ButtonHeld; }
        set { _ButtonHeld = value; }
    }

    private bool _ButtonUp;
    public bool ButtonUp
    {
        get { return _ButtonUp; }
        set { _ButtonUp = value; }
    }

    public Key(string new_button, GameObject p)
    {
        Button = new_button;
        player = p;

    }
	
	/// <summary>
    /// Update call for all the key states. If a state returns true, call the function for that state.
    /// </summary>
	public void UpdateKeys()
    {
        ButtonDown = Input.GetButtonDown(Button);
        ButtonHeld = Input.GetButton(Button);
        ButtonUp = Input.GetButtonUp(Button);

        if(GameManager.Instance.GameState == States.InGame || GameManager.Instance.GameState == States.Debug)
        {
            //If we tap a key.
            if (ButtonDown)
            {
                TapAction(player);
            }
            else if (ButtonHeld) //If we tap and hold a key.
            {
                HeldAction(player);
            }
            else if (ButtonUp) //If we release a key.
            {
                ReleaseAction(player);
            }
        }

    }

    /// <summary>
    /// Sets the function for a tap control.
    /// </summary>
    /// <param name="action_tap"></param>
    public void SetTapControl(TapA action_tap)
    {
        TapAction = action_tap;
    }

    /// <summary>
    /// Sets the function for a held control.
    /// </summary>
    /// <param name="action_held"></param>
    public void SetHeldControl(HeldA action_held)
    {
        HeldAction = action_held;
    }

    /// <summary>
    /// Sets the function for a release control.
    /// </summary>
    /// <param name="action_release"></param>
    public void SetReleaseControl(RelA action_release)
    {
        ReleaseAction = action_release;
    }

}
