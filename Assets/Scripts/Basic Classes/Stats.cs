using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public int id;
    public Roles role;
    private string roleName;
    //Basic stats
    private int str = 0;
    private int dex = 0;
    private int intel = 0;

    private int _miniGameScore = 0;
    public int miniGameScore
    {
        get
        {
            return _miniGameScore;
        }
        set
        {
            _miniGameScore = value;
        }
    }

    private int _score = 0;
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

	// Use this for initialization
	void Start () {
        setStats(role);
	}

    public void setStats(int s, int d, int i)
    {
        str = s;
        dex = d;
        intel = i;
    }

    //Set the class of the player via presets from a Roles object
    public void setStats(Roles role)
    {
        roleName = role.roleName;
        str = role.strength;
        dex = role.dexterity;
        intel = role.intelligence;
    }
        
    //Increments the player's mini game score by a set amount
    public void incrementMiniGameScore(int newScore)
    {
        miniGameScore += newScore;
    }

    //Sets the minigame score of the player.
    public void setMiniGameScore(int newScore)
    {
        miniGameScore = newScore;
    }

    //Sets the overall score of the player.
    public void setScore(int newScore)
    {
        score = newScore;
        resetMiniGameScore();
    }

    //Reset function for whenever a minigame is finished.
    public void resetMiniGameScore()
    {
        miniGameScore = 0;
    }


}
