using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {
    private float fillAmount;
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            //because its not always gonna be health before the :, 
            //so we take out everything before the colon
            if (valueText)
            {
                string[] temp = valueText.text.Split(':');
                valueText.text = temp[0] + ": " + (int)value; //make sure its lowercase v
                fillAmount = Map(value, 0, MaxValue, 0, 1);
            }
        }
    }
    
    
    
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
	}
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
            //Lerp it from one position (content.fillAmount) to another (fillAmount) over the lerp speed
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed); //will be updated by Value, lerp here
    }
    /*
    value - current
    inMin - lowest value
    inMax - max value
    outMin- - lowest value we are translating out (ie 0)
    outMax - max value we are translating out (ie 1 like 100%)
    */
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; 
        /*Example
        * (80 - 0) * (1 - 0) / (100 - 0) + 0
        * 80 / 100 = 0.8 
        * Example 2
        * (78 - 0) * (1 - 0) / (230 - 0) + 0
        * 78 / 230 = 0.339 -> 0.34
        */
    }
}
