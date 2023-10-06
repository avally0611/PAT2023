using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IInteractable
{
   
    [SerializeField] private KitchenObjects kitchenObjects;
 
    public event EventHandler OnPlayerGrabbedObject;

    /*private KitchenObjectManager kitchenObjectManager;*/


    public void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObjectManager.SpawnKitchenObject(kitchenObjects, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }


        

    }



    public Transform GetTransform()
    {
        return transform;
    }

    public void KitchenAction(Player player)
    {
        //nothing needs to be done here
    }


    /*public Transform GetKitchenObjectFollowTransform()
    {
        //gets counter top point for attached counter
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObjectManager kitchenObjectManager)
    {
        this.kitchenObjectManager = kitchenObjectManager;
    }

    public KitchenObjectManager GetKitchenObjectManager()
    {
        return kitchenObjectManager;
    }

    public void ClearKitchenObject()
    {
        kitchenObjectManager = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObjectManager != null;
    }*/
}
