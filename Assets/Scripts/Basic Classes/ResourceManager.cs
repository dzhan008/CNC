using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {

    public Dictionary<string, GameObject> MiniGames { get; set; }

    public Dictionary<string, GameObject> Models { get; set; }

    public Dictionary<string, AudioClip> Audio { get; set; }


    void Awake()
    {
        MiniGames = new Dictionary<string, GameObject>();
        Models = new Dictionary<string, GameObject>();
        Audio = new Dictionary<string, AudioClip>();

        InitializeMiniGames();
        InitializeModels();
        InitializeAudio();
    }

	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeMiniGames()
    {
        MiniGames.Add("Swift Smiths", (GameObject)Resources.Load("Prefabs/Minigames/Swift Smiths/Swift Smiths"));
        MiniGames.Add("Stack Overflowed", (GameObject)Resources.Load("Prefabs/Minigames/Stack Overflowed/Stack Overflowed"));
        MiniGames.Add("Hungry Hungry Dragon", (GameObject)Resources.Load("Prefabs/Minigames/HHD/HHD"));
    }

    public void InitializeModels()
    {
        Models.Add("Archer", (GameObject)Resources.Load("Prefabs/Models/Archer"));
        Models.Add("FemaleMage", (GameObject)Resources.Load("Prefabs/Models/FemaleMage"));
        Models.Add("Swordsman", (GameObject)Resources.Load("Prefabs/Models/Swordsman"));

    }

    public void InitializeAudio()
    {
        Audio.Add("Stack Overflowed", (AudioClip)Resources.Load("Music/Book Mini Game"));
        Audio.Add("Hungry Hungry Dragon", (AudioClip)Resources.Load("Music/Dragon Chase"));
        Audio.Add("Title", (AudioClip)Resources.Load("Music/Title Theme_Complete"));
        Audio.Add("Victory", (AudioClip)Resources.Load("Music/Title Theme_Complete"));
    }
}
