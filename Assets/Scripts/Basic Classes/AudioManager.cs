using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField]
    private AudioSource BackgroundMusic;
    [SerializeField]
    private AudioSource SoundEffect;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySong(string name)
    {
        BackgroundMusic.clip = ResourceManager.Instance.Audio[name];
        BackgroundMusic.Play();
    }

    public void PlaySfx(string name)
    {
        SoundEffect.PlayOneShot(ResourceManager.Instance.Audio[name]);
    }

    public void StopSounds()
    {
        BackgroundMusic.Stop();
        SoundEffect.Stop();
    }
}   
