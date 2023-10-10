using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter, IInteractable
{
    [SerializeField] DeliveryManager manager;

    public void InteractPrimary(Player player)
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

    public void InteractSecondary(Player player)
    {
       //nothing special here
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
