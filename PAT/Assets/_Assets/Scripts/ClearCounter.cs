using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IInteractable
{
   
    [SerializeField] private KitchenObjects kitchenObjects;
  


   //private KitchenObjectManager kitchenObjectManager;



    public void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //counter has no object
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player carrying nothing - do nothing
            }
        }
        else
        {
            //there is object on counter

            if (player.HasKitchenObject())
            {
                //do nothing
            }
            else 
            {
                //give to player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }



    }


    public Transform GetTransform()
    {
        return transform;
    }

    public void KitchenAction(Player player)
    {
        //
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