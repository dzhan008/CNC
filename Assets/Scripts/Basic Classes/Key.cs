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
	
	// Update is called once per frame
	public void UpdateKeys()
    {
        ButtonDown = Input.GetButtonDown(Button);
        ButtonHeld = Input.GetButton(Button);
        ButtonUp = Input.GetButtonUp(Button);

        //If we tap a key.
        if (ButtonDown)
        {
            TapAction(player);
        }
        else if (ButtonHeld) //If we tap and hold a key.
        {
            HeldAction(player);
        }
        else if(ButtonUp) //If we release a key.
        {
            ReleaseAction(player);
        }
    }

    public void SetTapControl(TapA action_tap)
    {
        TapAction = action_tap;
    }

    public void SetHeldControl(HeldA action_held)
    {
        HeldAction = action_held;
    }

    public void SetReleaseControl(RelA action_release)
    {
        ReleaseAction = action_release;
    }

}
