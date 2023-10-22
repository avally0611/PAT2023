using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

//plate is a type of kitchen object - so behave like kitchen object but diff functions
//class for a plate object with all the methods
public class PlateKitchenObject : KitchenObjectManager
{
    [SerializeField] private KitchenObjects[] validKitchenObjectsArr;

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    //data related to eveny - when ingredient add - data says what ingredient was added 
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjects kitchenObjects;
    }

    private KitchenObjects[] kitchenObjectsArr;
    private int count;


    private void Awake()
    {
        kitchenObjectsArr = new KitchenObjects[10];
    }

    //checks if you plate already has object and checks if you try to add things like a whole tomato
    
    //can only add chopped things (as according to array)
    public bool TryAddIngredient(KitchenObjects kitchenObjects)
    {
        if (!validKitchenObjectsArr.Contains(kitchenObjects))
        {
            return false;
        }

        
        if (kitchenObjectsArr.Contains(kitchenObjects))
        {
            //already has this type of ingredient
            return false;
        }
        else
        {
            kitchenObjectsArr[count] = kitchenObjects;
            count++;

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjects = kitchenObjects });

            return true;
        }
    }

    public KitchenObjects[] GetKitchenObjectsArr()
    {
        return kitchenObjectsArr;
    }

    public int GetArrCount()
    {
        return count;
    }


   
}
    

