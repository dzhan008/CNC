/*
Created By: David Zhang
Description: This is the base class for all minigames. Contains functions needed to make a basic minigame.
Requirements: This must be attached to the prefab of where your minigame should be.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MiniGamePerspective
{
    SideScroller = 1,
    TopDownHunting,
    TopDownHorse
}

public abstract class Minigame : MonoBehaviour
{
    [SerializeField]
    protected MiniGamePerspective Perspective;

    /* Timer Related Variables */
    protected float TimeLeft = 0;
    protected float TimeLimit;
    protected bool TimerOn = false;
    protected bool Finished = false;

    /* Instruction Screen Gameobjects */
    [SerializeField]
    protected GameObject InstructionPanel;
    [SerializeField]
    protected GameObject RulesPanel;
    [SerializeField]
    protected GameObject ControlsPanel;
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
    /// Handles events for when the minigame ends.
    /// </summary>
    public void GameEnd()
    {
        int highest_score = 0;
        int winning_ID = 1;
        List<int> winning_IDs = new List<int>();
        Dictionary<int, KeyValuePair<GameObject,Stats>> Players = GameManager.Instance.Players;
        for(int i = 1; i <= Players.Count; i++)
        {
            if (Players[i].Value.MiniGameScore >= highest_score)
            {
                highest_score = Players[i].Value.MiniGameScore;
                winning_ID = i;
            }
        }
        for (int i = 1; i <= Players.Count; i++)
        {
            if (Players[i].Value.MiniGameScore == highest_score)
            {
                winning_IDs.Add(i);
                Players[i].Value.ResetMiniGameScore();
            }
        }
        for(int i = 0; i < winning_IDs.Count; i++)
        {
            string output = "Player ";
            Players[winning_IDs[i]].Value.AddScore(1);
            output += i + ", ";
            Debug.Log(output);
        }
        GameManager.Instance.DisplayProgress(); //Starts a new minigame. May modify to change the state of the game manager instead.

    }

                                                                                               /* Minigame Functions */
    public abstract void OnStart();
    public void OnRules()
    {
        RulesPanel.SetActive(true);
        ControlsPanel.SetActive(false);
    }
    public void OnControls()
    {
        RulesPanel.SetActive(false);
        ControlsPanel.SetActive(true);
    }

                                                                                                /* Key Actions */

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

}
