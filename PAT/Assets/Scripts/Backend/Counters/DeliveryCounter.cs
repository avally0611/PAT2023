using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter, IInteractable
{
    [SerializeField] DeliveryManager manager;

    //if E is pressed - check if played holding plate with kitchen objects then go to Delivery Manager and check if ingredients match any of the waiting recipes
    public void InteractPrimary(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObjectManager().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plate

                manager.DeliverRecipe(plateKitchenObject);
                
                //once they match - get rid of object - object is "delivered"
                player.GetKitchenObjectManager().DestroySelf();
            }
        }
    }

    //interface method - have to have 
    public void InteractSecondary(Player player)
    {
       //nothing special here
    }

    ////same as usual - position of counter - see if player close enough to interact
    public Transform GetTransform()
    {
        return transform;
    }
}
