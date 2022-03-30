using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandle : MonoBehaviour
{
     [SerializeField] private AudioSource audioSource = null;

    bool state  = false;

    
    public void StopMusic()
    {
        audioSource.Pause();
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void ChangeStateMusic()
    {
        if (!state)
        {
             StopMusic();  
             state = true;
        }
        else if (state)
        {
            PlayMusic();
            state = false;   
        }
    }
}
