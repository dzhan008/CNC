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

	//For Bar
    [SerializeField]
	private BarScript SprintBar;

    [SerializeField]
    private float sprintMaxVal;

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
	//End For Bar
	/// <summary>
	/// Description: Calculates the duration of sprint
	/// </summary>
	float sprintDurationCalc(Stats Player)
	{
		return 1;
	}

	/// <summary>
	/// Description: Calculates the cooldown of chicken charges
	/// </summary>
	float chickenChargeRateCalc(Stats Player)
	{
		return 1;
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
		return 5;
	}
	//Player stats

	public void Initialize(Stats player, BarScript sprintBar)
    {
        SprintBar = sprintBar;
        this.SprintMaxVal = sprintMaxVal;
        this.SprintCurrentVal = sprintCurrentVal;
        PSkills = new Dictionary<string, float>();
		//Strength 

		PSkills.Add("sprintbar", 100);
		PSkills.Add("sprintChargeRate", 1);
		PSkills.Add("sprintDuration", sprintDurationCalc(player));
        PSkills.Add("sprintStartTime", 0);
		PSkills.Add("isSprint", 0);
		//Intelligence
		PSkills.Add("chickenBar", 100);
		PSkills.Add("chickenChargeRate", chickenChargeRateCalc(player));
		PSkills.Add("chickenCharges", 3);
		//Dexterity
		PSkills.Add("jumpHeight", jumpHeightCalc(player));
		PSkills.Add("baseSpeed", baseSpeedCalc(player));
    }
	//public float returnDictionary(string key){
	//	return PSkills [key];
	//}

}
