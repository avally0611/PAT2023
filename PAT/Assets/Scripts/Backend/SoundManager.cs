using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefSO audioClipsRefSO;
    
    [SerializeField] DeliveryManager deliveryManager;
    [SerializeField] DeliveryCounter deliveryCounter;
    [SerializeField] Player player;

    float volume;


    private void Start()
    {
        deliveryManager.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;

        deliveryManager.OnRecipeFailure += DeliveryManager_OnRecipeFailure;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        player.OnPickedSomething += Player_OnPickedSomething;

        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;

        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipsRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipsRefSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefSO.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        //cutting counter was sender - class that fired off event
        CuttingCounter cuttingCounter = sender as CuttingCounter;   
        PlaySound(audioClipsRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailure(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 2f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArr, Vector3 position, float volume = 2f)
    {
        PlaySound(audioClipArr[Random.Range(0, audioClipArr.Length)], position, this.volume);
        
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipsRefSO.footStep, position, volume);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipsRefSO.warning, position);
    }

    public void ChangeVolume(float volume)
    {
        this.volume = volume * 0.01f;
    }
}
