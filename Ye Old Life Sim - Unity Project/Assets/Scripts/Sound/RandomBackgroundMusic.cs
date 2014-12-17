using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomBackgroundMusic : MonoBehaviour 
{
    public AudioClip m_BackGroundSound1;
    public AudioClip m_BackGroundSound2;
    public AudioClip m_BackGroundSound3;
    
    private List<AudioClip> backGroundSoundArray_ = new List<AudioClip>();

    private int randomNumber_ = 0;
    private int maxListSize = 3;


	void Start () 
    {
        PickSongToPlay();
	}
	
	void Update ()
    {  
        if(!audio.isPlaying)
        {        
            //once audio is not playing, get another track to play
            PickSongToPlay();
        }    
	}

    public void PickSongToPlay()
    {
        randomNumber_ = Random.Range(1, 4);     //gets a number between 1 and 3
        if(randomNumber_ == 1)
        {
            //if the list does not have this background sound in it, then add it to it and play the music
            if(!backGroundSoundArray_.Contains(m_BackGroundSound1))
            {
                backGroundSoundArray_.Add(m_BackGroundSound1);
                MakeSound(m_BackGroundSound1);
            }
            else
            {
                //call random number again so you can get a different sound file 
                randomNumber_ = Random.Range(1, 4);     //gets a number between 1 and 3
            }
        }
        else if(randomNumber_ == 2)
        {
            //if the list does not have this background sound in it, then add it to it and play the music
            if (!backGroundSoundArray_.Contains(m_BackGroundSound2))
            {
                backGroundSoundArray_.Add(m_BackGroundSound2);
                MakeSound(m_BackGroundSound2);
            }
            else
            {
                //call random number again so you can get a different sound file 
                randomNumber_ = Random.Range(1, 4);     //gets a number between 1 and 3
            }
        }
        else if(randomNumber_ == 3)
        {
            //if the list does not have this background sound in it, then add it to it and play the music
            if (!backGroundSoundArray_.Contains(m_BackGroundSound3))
            {
                backGroundSoundArray_.Add(m_BackGroundSound3);
                MakeSound(m_BackGroundSound3);
            }
            else
            {
                //call random number again so you can get a different sound file 
                randomNumber_ = Random.Range(1, 4);     //gets a number between 1 and 3
            }
        }

        //if the list has reached its max size it means you can play all the songs that have been used
        if (backGroundSoundArray_.Count == maxListSize)
        {       
            randomNumber_ = Random.Range(1, 4);     //random number between 1 and 3

            //play a music track based on what random number was picked
            if(randomNumber_ == 1)
            {
                MakeSound(m_BackGroundSound1);
            }
            else if(randomNumber_ == 2)
            {
                MakeSound(m_BackGroundSound2);
            }
            else if(randomNumber_ == 3)
            {
                MakeSound(m_BackGroundSound3);
            }
        }
    }

    private void MakeSound(AudioClip originalClip)
    {
        //audio clips fire and forget themselves, so in order to keep track on whether or not it is playing this function will assign the audio to the clip giving to it
        audio.clip = originalClip;
        audio.Play();
    }
}
