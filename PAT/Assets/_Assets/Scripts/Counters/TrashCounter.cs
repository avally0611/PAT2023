using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter, IInteractable
{
    
    

    public void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObjectManager().DestroySelf();
        }
    }

    public void KitchenAction(Player player)
    {
        //no other specific action
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
