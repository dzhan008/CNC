using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerStat{

    [SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal; //current value of the stat (ie health, mana, etc)

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {

            currentVal = Mathf.Clamp(value, 0, MaxVal); //lower v, upper M
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {//set the max value of the bar
            this.maxVal = value;
            bar.MaxValue = maxVal;

        }
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
