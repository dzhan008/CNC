using UnityEngine;
using System.Collections;

public class DragonPlayer : MonoBehaviour {
    [SerializeField]
    private PlayerStat sprint;

    //This needs to be called before our bar script
    //so much call Awake instead of Start
	private void Awake()
    {
        sprint.Initialize();
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            sprint.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            sprint.CurrentVal += 10;
        }
	
	}
}
