using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
[Serializable]
public class PlayerStat: MonoBehaviour {
    public Dictionary<string, float> PSkills { get; set; }
    //public Dictionary<string, float> skills { get; set; }
    //Dictionary<string, float> PSkills = new Dictionary<string, float>();
    //For Cock Block
    [SerializeField]
    private CockBlockSpawn CockBlockSpawner;
	//For Bar
    [SerializeField]
	private BarScript SprintBar;
    [SerializeField]
    private BarScript ObstacleBar;

    [SerializeField]
    private float sprintMaxVal;
    [SerializeField]
    private float obstacleMaxVal;

    [SerializeField]
    private float sprintCurrentVal; //current value of the stat (ie health, mana, etc)
    public float SprintCurrentVal
    {
        get
        {
			return sprintCurrentVal;
        }

        set
        {

			sprintCurrentVal = Mathf.Clamp(value, 0, SprintMaxVal); //lower v, upper M
			SprintBar.Value = sprintCurrentVal;
        }
    }

    public float SprintMaxVal
    {
        get
        {
			return sprintMaxVal;
        }

        set
        {//set the max value of the bar
			this.sprintMaxVal = value;
			SprintBar.MaxValue = sprintMaxVal;

        }
    }
    [SerializeField]
    private float obstacleCurrentVal; //current value of the stat (ie health, mana, etc)
    public float ObstacleCurrentVal
    {
        get
        {
            return obstacleCurrentVal;
        }

        set
        {

            obstacleCurrentVal = Mathf.Clamp(value, 0, ObstacleMaxVal); //lower v, upper M
            ObstacleBar.Value = obstacleCurrentVal;
        }
    }

    public float ObstacleMaxVal
    {
        get
        {
            return obstacleMaxVal;
        }

        set
        {//set the max value of the bar
            this.obstacleMaxVal = value;
            ObstacleBar.MaxValue = obstacleMaxVal;

        }
    }
    //End For Bar
    /// <summary>
    /// Description: Calculates the duration of sprint
    /// </summary>
    float sprintDurationCalc(Stats Player)
	{
		return 6;
	}
    float speedReductionCalc(Stats Player)
    {
        return 0.03f;
    }
    float slowDurationCalc(Stats Player)
    {
        return 2;
    }

	/// <summary>
	/// Description: Calculates the cooldown of chicken charges
	/// </summary>
	float chickenChargeRateCalc(Stats Player)
	{
		return 0.2f;
	}

	/// <summary>
	/// Description: Calculates the bonus jump height
	/// </summary>
	float jumpHeightCalc(Stats Player)
	{
		return 400;
	}

	float baseSpeedCalc(Stats Player)
	{
		return 5.1f;
	}

    public void spawnCockBlockObstacle()
    {
        CockBlockSpawner.spawnCockBlock();
    }
	//Player stats

	public void Initialize(Stats player, BarScript sprintBar, BarScript obstacleBar)
    {
        //Initialize Sprint Bar
        SprintBar = sprintBar;
        this.SprintMaxVal = sprintMaxVal;
        this.SprintCurrentVal = sprintCurrentVal;

        //Initialize Obstacle Bar
        ObstacleBar = obstacleBar;
        this.ObstacleMaxVal = obstacleMaxVal;
        this.ObstacleCurrentVal = obstacleCurrentVal;
      
        PSkills = new Dictionary<string, float>();

		//Dexterity 
		PSkills.Add("sprintbar", 100);
        PSkills.Add("sprintSpeedAdd", 0);
		PSkills.Add("sprintChargeRate", 1);
		PSkills.Add("sprintDuration", sprintDurationCalc(player));
        PSkills.Add("sprintStartTime", 0);
		PSkills.Add("isSprint", 0);
		//Intelligence
		PSkills.Add("chickenBar", 100);
		PSkills.Add("chickenChargeRate", chickenChargeRateCalc(player));
		PSkills.Add("chickenCharges", 3);
		//Strength
		PSkills.Add("jumpHeight", jumpHeightCalc(player));
		PSkills.Add("baseSpeed", baseSpeedCalc(player));
        PSkills.Add("playerSlowAdd", 0);
        PSkills.Add("speedReduction", speedReductionCalc(player));
        PSkills.Add("sprintSlowDuration", slowDurationCalc(player));
    }
}
