using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
    public string leveltoload;
	// Use this for initialization
	void Start () {
	
	}

    public string scene;
    public void ChangeToScene(string scene)
    { //function to change scenes/levels.
        Application.LoadLevel(scene);         //
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update () {
	
	}
}
