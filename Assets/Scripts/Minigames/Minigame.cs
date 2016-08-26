/*
Created By: David Zhang
Description: This is the base class for all minigames. Contains functions needed to make a basic minigame.
Requirements: This must be attached to the prefab of where your minigame should be.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour
{

    protected float TimeLeft = 0;
    protected float TimeLimit;
    protected bool TimerOn;
    protected bool Finished = false;

    //Used to display the timer, if needed.
    public Text Timer;

    /// <summary>
    /// This is a simple timer that will subtract the time left each second.
    /// If set to one, it'll count down like an actual clock.
    /// </summary>
    /// <returns></returns>
    public int CountDown(float rate)
    {
            TimeLeft -= Time.deltaTime * rate;
            Timer.text = "Time: " + (int)TimeLeft;
            if (TimeLeft < 0)
            {
                return -1;
            }
            return 0;

    }
    /// <summary>
    /// Sets the time remaining.
    /// </summary>
    /// <param name="new_time"></param>
    public void SetTime(int new_time)
    {
        TimeLeft = new_time;
    }

    /// <summary>
    /// Sets the controls for the minigame.
    /// </summary>
    /// <param name="player"></param>
    public void SetControls(GameObject player)
    {
        player.GetComponent<Controls>().SetControls(LeftAction, CenterAction, RightAction);
    }

    /// <summary>
    /// The logic for a left key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void LeftAction(GameObject player);

    /// <summary>
    /// The logic for a center key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void CenterAction(GameObject player);

    /// <summary>
    /// The logic for a right key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void RightAction(GameObject player);

    /// <summary>
    /// Handles events for when the minigame ends.
    /// </summary>
    public abstract void GameEnd();

}
