using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
[Serializable]
public class PlayerStat : MonoBehaviour
{
    public Dictionary<string, float> PSkills { get; set; }
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
        float playerDex = Player.Dex;
        if (playerDex > 7) return 4f;
        else if (playerDex > 4) return 3.25f;
        return 2.5f;
    }
    float speedReductionCalc(Stats Player)
    {
        //Assuming scale 1-10
        float playerStr = Player.Str;
        if (playerStr > 7) return 0.01f;
        else if (playerStr > 4) return 0.02f;
        return 0.025f;
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
        float playerIntel = Player.Intel;
        if (playerIntel > 7) return 0.8f;
        else if (playerIntel > 4) return 0.42f;
        return 0.3f;
    }

    /// <summary>
    /// Description: Calculates the bonus jump height
    /// </summary>
    float jumpHeightCalc(Stats Player)
    {
        return 530;
    }

    float baseSpeedCalc(Stats Player)
    {
        return 5.2f;
    }

    public void spawnCockBlockObstacle()
    {
        CockBlockSpawner.spawnCockBlock();
    }
    //Player stats
    public void Initialize(Stats player, BarScript sprintBar, BarScript obstacleBar,
        CockBlockSpawn CBSpawner,
        int sprintMax, int sprintCur, int obsMax, int obsCur)
    {
        CockBlockSpawner = CBSpawner;
        //Initialize Sprint Bar
        SprintBar = sprintBar;
        sprintMaxVal = sprintMax;
        sprintCurrentVal = sprintCur;
        this.SprintMaxVal = sprintMaxVal;
        this.SprintCurrentVal = sprintCurrentVal;

        //Initialize Obstacle Bar
        ObstacleBar = obstacleBar;
        obstacleMaxVal = obsMax;
        obstacleCurrentVal = obsCur;
        this.ObstacleMaxVal = obstacleMaxVal;
        this.ObstacleCurrentVal = obstacleCurrentVal;

        PSkills = new Dictionary<string, float>();

        PSkills.Add("sprintbar", 100);
        PSkills.Add("sprintSpeedAdd", 0);
        PSkills.Add("sprintChargeRate", 1);
        PSkills.Add("sprintDuration", sprintDurationCalc(player)); //DEX
        PSkills.Add("sprintStartTime", 0);
        PSkills.Add("isSprint", 0);

        PSkills.Add("chickenBar", 100);
        PSkills.Add("chickenChargeRate", chickenChargeRateCalc(player)); //INT
        PSkills.Add("chickenCharges", 3);

        PSkills.Add("jumpHeight", jumpHeightCalc(player));
        PSkills.Add("baseSpeed", baseSpeedCalc(player));
        PSkills.Add("playerSlowAdd", 0);
        PSkills.Add("speedReduction", speedReductionCalc(player)); //STR
        PSkills.Add("sprintSlowDuration", slowDurationCalc(player));
    }
}
