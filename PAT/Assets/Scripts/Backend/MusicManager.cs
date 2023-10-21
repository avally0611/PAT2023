using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();


    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume * 0.01f;    
        
    }
}
