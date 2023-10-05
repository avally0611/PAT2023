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
            //clones specified object and retunrs at spec point
            Transform kitchenObjTransform = Instantiate(kitchenObjects.prefab);

            //placing kitchen object in player hands
            kitchenObjTransform.GetComponent<KitchenObjectManager>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }


        

    }



    public Transform GetTransform()
    {
        return transform;
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

    public KitchenObjectManager GetKitchenObject()
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
