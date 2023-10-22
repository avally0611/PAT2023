using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IInteractable, IHasProgress
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    //stores info about event that listeners can access
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    private State state;
    private float fryingTimer;
    private float burningTimer;

    //ie patty - cooked patty (contains visuals)
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    
    

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private void Start()
    {
        state = State.Idle; 
    }

    //basically checks all the cooking states and when a specific state is triggered it does specific actions - more info below
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;

                case State.Frying:

                    //start frying timer
                    fryingTimer += Time.deltaTime;

                    //populate loading bar - frying
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = fryingTimer / fryingRecipeSO.fryingTimerMax });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //patty is fried
                        GetKitchenObjectManager().DestroySelf();

                        //spawns fried patty
                        KitchenObjectManager.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        state = State.Fried;

                        
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSO(GetKitchenObjectManager().GetKitchenObjects());

                        //loading bar - burning
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state }); 

                    }

                    break;

                case State.Fried:

                    //start burning timer
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = burningTimer / burningRecipeSO.burningTimerMax });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        //patty is fried
                        GetKitchenObjectManager().DestroySelf();

                        KitchenObjectManager.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = 1f });

                    }

                    break;

                case State.Burned:
                    break;
            }
        }
    }

    //when E is pressed - either drop patty or pick it up
    public void InteractPrimary(Player player)
    {
        //counter has no object
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                //gets input obj (raw patty) - gives recipe output options
                if (HasRecipeWithInput(player.GetKitchenObjectManager().GetKitchenObjects()))
                {
                    //if player has a object that cna be fried, then you can drop 
                    player.GetKitchenObjectManager().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObjectManager().GetKitchenObjects());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = fryingTimer / fryingRecipeSO.fryingTimerMax });

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
                //checks player has plate - put patty on plate visual 
                if (player.GetKitchenObjectManager().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    //might change this  - basically checks if you have only added one type of ingredieant
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObjectManager().GetKitchenObjects()))
                    {
                        GetKitchenObjectManager().DestroySelf();

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = 1f });

                    }

                }

            }
            else
            {
                //give to player
                GetKitchenObjectManager().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = 1f });
            }
        }
    }


    //uses below method and just output if there is a frying recipe for object
    private bool HasRecipeWithInput(KitchenObjects inputKitchenObjects)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjects);

        return fryingRecipeSO != null;


    }

    //gets prefab and input/output (patty - cooked patty)
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjects inputKitchenObjects)
    {
        for (int i = 0; i < fryingRecipeSOArray.Length; i++)
        {
            if (fryingRecipeSOArray[i].input == inputKitchenObjects)
            {
                return fryingRecipeSOArray[i];
            }
        }
        return null;

    }

    //gets prefab and input/output (patty - burned patty)
    private BurningRecipeSO GetBurningRecipeSO(KitchenObjects inputKitchenObjects)
    {
        for (int i = 0; i < burningRecipeSOArray.Length; i++)
        {
            if (burningRecipeSOArray[i].input == inputKitchenObjects)
            {
                return burningRecipeSOArray[i];
            }
        }
        return null;

    }

    //usual
    public Transform GetTransform()
    {
        return transform;
    }

    void IInteractable.InteractSecondary(Player player)
    {
        //nothing here
    }

    //checks if state is fried - used for animations and sounds
    public bool isFried()
    {
        return state == State.Fried;
    }
}
