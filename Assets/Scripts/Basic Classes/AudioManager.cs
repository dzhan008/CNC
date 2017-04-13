using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSong(string name)
    {
        GetComponent<AudioSource>().clip = ResourceManager.Instance.Audio[name];
        GetComponent<AudioSource>().Play();
    }
}
