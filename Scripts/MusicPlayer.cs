using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip[] clips;
    private AudioSource audioSource;
    private int lastSong = 0;
    
    // Use this for initialization
	void Start () {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
        if (PlayerPrefs.HasKey("Volume"))
            audioSource.volume = PlayerPrefs.GetFloat("Volume");
        DontDestroyOnLoad(transform.parent);
	}

    private int GetRandomClip()
    {
        int randomIndex = lastSong;
        while (randomIndex == lastSong)
        {
            randomIndex = Random.Range(0, clips.Length);
        }
        lastSong = randomIndex;
        return randomIndex;

        //return clips[Random.Range(0, clips.Length)];
    }
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying)
        {
            audioSource.clip = clips[GetRandomClip()];
            audioSource.Play(); 
        }
	}
}
