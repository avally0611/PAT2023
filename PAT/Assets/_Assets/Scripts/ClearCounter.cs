using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    
    public GameObject Selected;

    public void Interact(Transform interactableObjectTransform)
    {
        DisableOtherCounters();

        Selected.SetActive(true);  


    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void DisableOtherCounters()
    {
        //makes an array of all counter objects
        ClearCounter[] counters = FindObjectsOfType<ClearCounter>();

        for (int i = 0; i < counters.Length; i++)
        {
            if (counters[i] != this)
            {
                counters[i].Selected.SetActive(false);
            }

        }

    }

}