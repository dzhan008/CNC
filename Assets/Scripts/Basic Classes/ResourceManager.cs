using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {

    public Dictionary<string, GameObject> MiniGames { get; set; }

    public Dictionary<string, GameObject> Models { get; set; }


	// Use this for initialization
	void Start ()
    {
        MiniGames = new Dictionary<string, GameObject>();
        Models = new Dictionary<string, GameObject>();

        InitializeMiniGames();
        InitializeModels();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeMiniGames()
    {
        MiniGames.Add("Swift Smiths", (GameObject)Resources.Load("Prefabs/Minigames/Swift Smiths/Swift Smiths/"));
        MiniGames.Add("Stack Overflowed", (GameObject)Resources.Load("Prefabs/Minigames/Stack Overflowed/Stack Overflowed"));
    }

    public void InitializeModels()
    {
        Models.Add("Archer", (GameObject)Resources.Load("Prefabs/Models/Archer"));
        Models.Add("FemaleMage", (GameObject)Resources.Load("Prefabs/Models/FemaleMage"));
        Models.Add("Swordsman", (GameObject)Resources.Load("Prefabs/Models/Swordsman"));

    }
}
