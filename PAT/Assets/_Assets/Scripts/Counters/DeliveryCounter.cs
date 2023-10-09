using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter, IInteractable
{
    [SerializeField] DeliveryManager manager;

    public void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObjectManager().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plate

                manager.DeliverRecipe(plateKitchenObject);
                
                player.GetKitchenObjectManager().DestroySelf();
            }
        }
    }

    public void KitchenAction(Player player)
    {
       //nothing special here
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
