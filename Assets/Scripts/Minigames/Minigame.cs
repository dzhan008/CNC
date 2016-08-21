using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour {

    protected float timeLeft = 0;
    protected float timeLimit;
    protected bool timerOn;
    protected bool finished = false;

    //Used to display the timer, if needed.
    public Text timer;

    //This is a simple timer that will subtract the time left each second.
    public int countDown()
    {
            timeLeft -= Time.deltaTime;
            timer.text = "Time: " + (int)timeLeft;
            if (timeLeft < 0)
            {
                return -1;
            }
            return 0;

    }

    //Set the time remaining.
    public void setTime(int newTime)
    {
        timeLeft = newTime;
    }

    //Sets the controls for the minigame.
    public void setControls(GameObject player)
    {
        player.GetComponent<Controls>().setControls(leftAction, centerAction, rightAction);
    }

    public abstract void leftAction(GameObject player);

    public abstract void centerAction(GameObject player);

    public abstract void rightAction(GameObject player);

    //Call this when the minigame is finished
    public abstract void GameEnd();

}
