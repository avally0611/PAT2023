using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IInteractable
{
   
    [SerializeField] private KitchenObjects kitchenObjects;
  


   //private KitchenObjectManager kitchenObjectManager;



    public void InteractPrimary(Player player)
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
                if (player.GetKitchenObjectManager().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    //might change this  - basically checks if you have only added one type of ingredieant
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObjectManager().GetKitchenObjects()))
                    {
                        GetKitchenObjectManager().DestroySelf();
                    }

                }
                else               
                {
                    //player not carrying plate but smth else while theres plate on counter
                    if (GetKitchenObjectManager().TryGetPlate(out plateKitchenObject))
                    {
                        //counter holding player
                        plateKitchenObject.TryAddIngredient(player.GetKitchenObjectManager().GetKitchenObjects());
                        player.GetKitchenObjectManager().DestroySelf();
                    }
                }
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

    void IInteractable.InteractSecondary(Player player)
    {
       //nothing here
    }
}