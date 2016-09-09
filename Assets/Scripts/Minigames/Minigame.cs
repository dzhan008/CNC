/*
Created By: David Zhang
Description: This is the base class for all minigames. Contains functions needed to make a basic minigame.
Requirements: This must be attached to the prefab of where your minigame should be.
*/

using UnityEngine;
using System.Collections;

public abstract class Minigame : MonoBehaviour
{

    protected float TimeLeft = 0;
    protected float TimeLimit;
    protected bool TimerOn = false;
    protected bool Finished = false;

    /// <summary>
    /// This is a simple timer that will subtract the time left each second.
    /// If set to one, it'll count down like an actual clock.
    /// </summary>
    /// <returns></returns>
    public int CountDown(float rate)
    {
            TimeLeft -= Time.deltaTime * rate;
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
        player.GetComponent<Controls>().SetTapControls(UpTapAction, LeftTapAction, CenterTapAction, RightTapAction);
        player.GetComponent<Controls>().SetHeldControls(UpHeldAction, LeftHeldAction, CenterHeldAction, RightHeldAction);
        player.GetComponent<Controls>().SetReleaseControls(UpRelAction, LeftRelAction, CenterRelAction, RightRelAction);
    }

    /// <summary>
    /// The logic for a up key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void UpTapAction(GameObject player);

    /// <summary>
    /// The logic for a left key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void LeftTapAction(GameObject player);

    /// <summary>
    /// The logic for a center key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void CenterTapAction(GameObject player);

    /// <summary>
    /// The logic for a right key press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void RightTapAction(GameObject player);

    /// <summary>
    /// The logic for a up key held press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void UpHeldAction(GameObject player);

    /// <summary>
    /// The logic for a left key held press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void LeftHeldAction(GameObject player);

    /// <summary>
    /// The logic for a center key held press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void CenterHeldAction(GameObject player);

    /// <summary>
    /// The logic for a right key held press.
    /// </summary>
    /// <param name="player"></param>
    public abstract void RightHeldAction(GameObject player);

    
    /// <summary>
    /// The logic for releasing the up key.
    /// </summary>
    /// <param name="player"></param>
    public abstract void UpRelAction(GameObject player);

    /// <summary>
    /// The logic for releasing the left key.
    /// </summary>
    /// <param name="player"></param>
    public abstract void LeftRelAction(GameObject player);

    /// <summary>
    /// The logic for releasing the center key.
    /// </summary>
    /// <param name="player"></param>
    public abstract void CenterRelAction(GameObject player);

    /// <summary>
    /// The logic for releasing the right key.
    /// </summary>
    /// <param name="player"></param>
    public abstract void RightRelAction(GameObject player);

    /// <summary>
    /// Handles events for when the minigame ends.
    /// </summary>
    public abstract void GameEnd();

}
