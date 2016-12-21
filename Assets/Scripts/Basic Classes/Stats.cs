/*
Created By: David Zhang
Description: Contains necessary variables that store the player's stats in game.
Requirements: Player GameObject
*/

using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{

    public int Id;
    public Roles CurrentRole;
    private string RoleName;

    //Basic stats
    private int _Str = 0;
    public int Str
    {
        get
        {
            return _Str;
        }
        set
        {
            _Str = value;
        }
    }

    private int _Dex = 0;
    public int Dex
    {
        get
        {
            return _Dex;
        }
        set
        {
            _Dex = value;
        }
    }

    private int _Intel = 0;
    public int Intel
    {
        get
        {
            return _Intel;
        }
        set
        {
            _Intel = value;
        }
    }
        
    private int _MiniGameScore = 0;
    public int MiniGameScore
    {
        get
        {
            return _MiniGameScore;
        }
        set
        {
            _MiniGameScore = value;
        }
    }

    private int _Score = 0;
    public int Score
    {
        get
        {
            return _Score;
        }
        set
        {
            _Score = value;
        }
    }

	// Use this for initialization
	void Start ()
    {
        SetStats(CurrentRole);
	}

    public void SetStats(int s, int d, int i)
    {
        Str = s;
        Dex = d;
        Intel = i;
    }

    /// <summary>
    /// Set the class of the player via presets from a Roles object.
    /// </summary>
    /// <param name="role"></param>
    public void SetStats(Roles role)
    {
        RoleName = role.RoleName;
        Str = role.Strength;
        Dex = role.Dexterity;
        Intel = role.Intelligence;
    }

    /// <summary>
    /// Increments the player's mini game score by a set amount.
    /// </summary>
    /// <param name="new_score"></param>
    public void IncrementMiniGameScore(int new_score)
    {
        MiniGameScore += new_score;
    }

    /// <summary>
    /// Sets the minigame score of the player.
    /// </summary>
    /// <param name="new_score"></param>
    public void SetMiniGameScore(int new_score)
    {
        MiniGameScore = new_score;
    }

    /// <summary>
    /// Increment the player's current score by an amount.
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        Score += score;
    }

    /// <summary>
    /// Sets the overall score of the player.
    /// </summary>
    /// <param name="new_score"></param>
    public void SetScore(int new_score)
    {
        Score = new_score;
    }

    /// <summary>
    /// Reset function for whenever a minigame is finished.
    /// </summary>
    public void ResetMiniGameScore()
    {
        MiniGameScore = 0;
    }
}
