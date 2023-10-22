using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IInteractable, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    //making new event that comp listens to and then when it happens it goes to class to get extra info about it
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;

    //for cut sfx
    public static event EventHandler OnAnyCut;

    private int cuttingProgress;
    
    //resets static event - explained in base counter - static events keep prev state
    public static new void ResetStaticData()
    {
        OnAnyCut = null;
    }

    //basically does all the checks when player presses E by cutting counter - if hands full = drop object on counter OR if hands empty = pick up OR if plate = put obj on plate AND makes sure only cuttable objects are placed
    public void InteractPrimary(Player player)
    {
        if (!HasKitchenObject())
        {
            //counter has no object
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObjectManager().GetKitchenObjects()))
                {
                    //if player has a object that cna be cut, then you can drop on chopping board
                    player.GetKitchenObjectManager().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObjectManager().GetKitchenObjects());

                    //notification - smth happened, ? = checks its not null, if not null it sends a notification to code that is listening, its then saying that the progress is this: current/max
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax} );
                }
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

                    //basically checks if you have only added one type of ingredieant
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObjectManager().GetKitchenObjects()))
                    {
                        GetKitchenObjectManager().DestroySelf();
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

    //when player presses F - check what type object - chop (cue chopping animation and loading bar)
    public void InteractSecondary(Player player)
    {
        //only cut if the counter has object and object on counter can be cut according to recipe
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObjectManager().GetKitchenObjects()))
        {
            //for loading bar
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObjectManager().GetKitchenObjects());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });

            //every time you press "F" the cutting progress increases, so the chopped object will only show up once youve pressed the button as the same times specified in the Cutting Recipe scriptable object
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjects outputKitchenObject = GetOutputForInput(GetKitchenObjectManager().GetKitchenObjects());

                //cut it
                GetKitchenObjectManager().DestroySelf();

                KitchenObjectManager.SpawnKitchenObject(outputKitchenObject, this);
            }
            
        }
    }

    //basically checks what object is put down and gives cut version - uses scriptable objects (SORT OF LIKE CLASSES - STUDENT CLASS)
    private KitchenObjects GetOutputForInput(KitchenObjects inputKitchenObjects)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputKitchenObjects);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    //gives you cutting recipe according to object placed - e.g I place tomato I get tomato SO cutting recipe: IE tomato - tomato slices
    private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjects inputKitchenObjects)
    {
        for (int i = 0; i < cuttingRecipeSOArray.Length; i++)
        {
            if (cuttingRecipeSOArray[i].input == inputKitchenObjects)
            {
                return cuttingRecipeSOArray[i];
            }
        }
        return null;

    }

    //basically checks if object has a cutting recipe - ie can object be cut
    private bool HasRecipeWithInput(KitchenObjects inputKitchenObjects)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputKitchenObjects);

        return cuttingRecipeSO != null;


    }

    //same as usual - position of counter - see if player close enough to interact
    public Transform GetTransform()
    {
        return transform;
    }

   

    
}
