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
                player.GetKitchenObjectManager().SetKitchenObjectParent(this);
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
                GetKitchenObjectManager().SetKitchenObjectParent(player);
            }
        }



    }


    public Transform GetTransform()
    {
        return transform;
    }

    void IInteractable.KitchenAction(Player player)
    {
       //nothing here
    }
}