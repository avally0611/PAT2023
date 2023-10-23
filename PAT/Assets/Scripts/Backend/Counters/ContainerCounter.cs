using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IInteractable
{
   
    [SerializeField] private KitchenObjects kitchenObjects;
 
    public event EventHandler OnPlayerGrabbedObject;


    //when player presses 'E' by container - if theyre not holding object - give them object
    //takes in player obj to check if player holding item
    public void InteractPrimary(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObjectManager.SpawnKitchenObject(kitchenObjects, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }


        

    }

    //interface method - basically checks where counter is to see if player is within reach
    //returns transform of counter
    public Transform GetTransform()
    {
        return transform;
    }

    //there is no 'F' function for a clear counter - this is an Interface method - has to be here
    public void InteractSecondary(Player player)
    {
        //nothing needs to be done here
    }


 
}
