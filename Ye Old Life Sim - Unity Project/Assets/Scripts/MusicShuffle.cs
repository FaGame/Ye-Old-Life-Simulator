using UnityEngine;
using System.Collections;

public class MusicShuffle : MonoBehaviour 
{
    public AudioClip[] m_Music;

    public AudioClip m_Track1;
    public AudioClip m_Track2;
    public AudioClip m_Track3;
	
    //plays a random track when game starts
	void Start() 
    {
        if (!audio.playOnAwake)
        {
            audio.clip = m_Music[Random.Range(0, m_Music.Length)];
            audio.Play();
        }
	}
	
    //plays a new random track once 1st track is done playing
	void Update() 
    {
        if (!audio.isPlaying)
        {
            audio.clip = m_Music[Random.Range(0, m_Music.Length)];
            audio.Play();
        }
       
	}
}
