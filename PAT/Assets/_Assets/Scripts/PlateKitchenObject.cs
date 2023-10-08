using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

//plate is a type of kitchen object - so behave like kitchen object but diff functions
public class PlateKitchenObject : KitchenObjectManager
{
    [SerializeField] private KitchenObjects[] validKitchenObjectsArr;

    private KitchenObjects[] kitchenObjectsArr;
    private int count;

    private void Awake()
    {
        kitchenObjectsArr = new KitchenObjects[10];
    }

    public bool TryAddIngredient(KitchenObjects kitchenObjects)
    {
        if (!validKitchenObjectsArr.Contains(kitchenObjects))
        {
            return false;
        }

        //might change
        if (kitchenObjectsArr.Contains(kitchenObjects))
        {
            //already has this type of ingredient
            return false;
        }
        else
        {
            kitchenObjectsArr[count] = kitchenObjects;
            count++;
            return true;
        }
    }


   
}
    

