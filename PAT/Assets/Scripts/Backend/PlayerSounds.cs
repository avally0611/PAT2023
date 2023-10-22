using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handle player walking sound
public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    [SerializeField] SoundManager soundManager;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        //count down timer
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking())
            {
                float volume = 10f;
                soundManager.PlayFootstepsSound(player.transform.position, volume);
            }

            
        }
    }

}
