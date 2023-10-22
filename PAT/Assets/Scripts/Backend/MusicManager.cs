using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles music - got the music from the same youtube tutorial i followed (listed in specs or design docs)
public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();


    }

    public void ChangeVolume(float volume)
    {
        //volume is not normalised ie increase by 10% = increase by 0.01)
        audioSource.volume = volume * 0.01f;    
        
    }
}
