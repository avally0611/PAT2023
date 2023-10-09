using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IInteractable, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;

    private float fryingTimer;

    private float burningTimer;

    private FryingRecipeSO fryingRecipeSO;

    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle; 
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;

                case State.Frying:

                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalised = fryingTimer / fryingRecipeSO.fryingTimerMax });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //patty is fried
                        GetKitchenObjectManager().DestroySelf();

                        KitchenObjectManager.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSO(GetKitchenObjectManager().GetKitchenObjects());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state }); 

                    }

                    break;

                case State.Fried:

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

    public void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //counter has no object
            if (player.HasKitchenObject())
            {
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

    private KitchenObjects GetOutputForInput(KitchenObjects inputKitchenObjects)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjects);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipeWithInput(KitchenObjects inputKitchenObjects)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjects);

        return fryingRecipeSO != null;


    }

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


    public Transform GetTransform()
    {
        return transform;
    }

    void IInteractable.KitchenAction(Player player)
    {
        //nothing here
    }
}
