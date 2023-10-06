using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IInteractable
{
    [SerializeField] private KitchenObjects cutKitchenObjects;

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

    public void KitchenAction(Player player)
    {
        if (HasKitchenObject())
        {
            //cut it
            GetKitchenObject().DestroySelf();

            KitchenObjectManager.SpawnKitchenObject(cutKitchenObjects, this);
        }
    }


    public Transform GetTransform()
    {
        return transform;
    }

    
}
