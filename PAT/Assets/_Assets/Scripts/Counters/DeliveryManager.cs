using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailure;

    //spawn random list
    private static RecipeSO[] waitingRecipeSOArr;
    private static int arrCount = 0;

    

    [SerializeField] private RecipeListSO recipeListSO;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 2f;

    private int waitingRecipesMax = 6;

    private void Awake()
    {
        
        waitingRecipeSOArr = new RecipeSO[10];
    }

    private void Update()
    {
        if (gameManager.IsGamePlaying())
        {
            //timer is counting down so when it reaches 0 & based on frame rate so it varies
            spawnRecipeTimer -= Time.deltaTime;

            if (spawnRecipeTimer <= 0f)
            {
                //resets countdown to zero
                spawnRecipeTimer = spawnRecipeTimerMax;



                RecipeSO[] listOfRecipeSO = recipeListSO.recipeSOArr;

                if (arrCount < waitingRecipesMax)
                {
                    //Range is for floating numbers, Next is for integers
                    RecipeSO waitingRecipeSO = listOfRecipeSO[UnityEngine.Random.Range(0, listOfRecipeSO.Length)];
                    waitingRecipeSOArr[arrCount] = waitingRecipeSO;
                    arrCount++;


                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }


            }
        }
        
    }

    //maybe add smth with regards to tips - if it fulfills first order - tip amount between 5-10?
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        
        for (int i = 0; i < arrCount; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOArr[i];
            
            //have same num of ingredients
            if (waitingRecipeSO.kitchenObjectsArr.Length  == plateKitchenObject.GetArrCount() )
            {
                bool plateContentsMatchesRecipes = true;
                

                for (int j = 0; j < waitingRecipeSO.kitchenObjectsArr.Length; j++)
                {
                    //going through all ingredients in the recipe
                    bool ingredientFound = false;
                    for (int k = 0; k < plateKitchenObject.GetArrCount(); k++)
                    {
                        //going through all ingredients on plate


                        if (waitingRecipeSO.kitchenObjectsArr[j] == plateKitchenObject.GetKitchenObjectsArr()[k])
                        {
                            //ingredient matches
                            ingredientFound = true;

                            //used when conditon is met early
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //plate ingredients do not match recipe ingredients
                        plateContentsMatchesRecipes = false;
                    }
                }

                if (plateContentsMatchesRecipes)
                {
                    //player delivered correct recipe

                    Remove(i);

                    
                    OnRecipeCompleted?.Invoke(this, new EventArgs());
                    
                    OnRecipeSuccess?.Invoke(this, new EventArgs());
                    

                    return;
                }

            }
        }
        //no matches found - did not deliver correct recipe
        Debug.Log("incorrect recipe");
        OnRecipeFailure?.Invoke(this, new EventArgs());


    }

    private void Remove(int index)
    {
        for (int i = index + 1; i < arrCount; i++)
        {
            waitingRecipeSOArr[i - 1] = waitingRecipeSOArr[i];
        }

        arrCount--;

    }

    public RecipeSO[] GetWaitingRecipeSoArr()
    {
        return waitingRecipeSOArr;
    }

    public int GetArrCount()
    {
        return arrCount;
    }

   


}
