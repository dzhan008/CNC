using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    public int id;

    public delegate void leftA(GameObject player);
    public delegate void centerA(GameObject player);
    public delegate void rightA(GameObject player);

    leftA leftAction;
    centerA centerAction;
    rightA rightAction;

    private bool leftButtonDown;
    private bool leftButtonHeld;
    private bool leftButtonUp;

    private bool centerButtonDown;
    private bool centerButtonHeld;
    private bool centerButtonUp;

    private bool rightButtonDown;
    private bool rightButtonHeld;
    private bool rightButtonUp;

    string leftButton;
    string centerButton;
    string rightButton;

	// Use this for initialization
	void Start ()
    {
        leftButton = "Left Button " + id;
        centerButton = "Center Button " + id;
        rightButton = "Right Button " + id;
	}
	
	// Update is called once per frame
	void Update () {

        leftButtonDown = Input.GetButtonDown(leftButton);
        leftButtonHeld = Input.GetButton(leftButton);
        leftButtonUp = Input.GetButtonDown(leftButton);

        centerButtonDown = Input.GetButtonDown(centerButton);
        centerButtonHeld = Input.GetButton(centerButton);
        centerButtonUp = Input.GetButtonUp(centerButton);

        rightButtonDown = Input.GetButtonDown(rightButton);
        rightButtonHeld = Input.GetButton(rightButton);
        rightButtonUp = Input.GetButtonUp(rightButton);


        if (leftButtonHeld)
        {
            leftAction(this.gameObject);
        }
        else if (rightButtonHeld)
        {
            rightAction(this.gameObject);
        }
        else if (centerButtonHeld)
        {
            centerAction(this.gameObject);
        }

    }

    public void setControls(leftA actionLeft, centerA actionCenter, rightA actionRight)
    {
        leftAction = actionLeft;
        centerAction = actionCenter;
        rightAction = actionRight;
    }

    //Probably put some menu functions here?

}
