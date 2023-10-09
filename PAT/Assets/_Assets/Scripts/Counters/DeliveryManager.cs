using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    //spawn random list
    private RecipeSO[] waitingRecipeSOArr;
    private int arrCount;

    [SerializeField] private RecipeListSO recipeListSO;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private int waitingRecipesMax = 4;

    private void Awake()
    {
        arrCount = 0;
        waitingRecipeSOArr = new RecipeSO[10];
    }

    private void Update()
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
                Debug.Log(arrCount);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }


        }
    }

    //maybe add smth with regards to tips - if it fulfills first order - tip amount between 5-10?
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        Debug.Log(arrCount);
        for (int i = 0; i < arrCount; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOArr[i];
            
            //have same num of ingredients
            if (waitingRecipeSO.kitchenObjectsArr.Length  == plateKitchenObject.GetKitchenObjectsArr().Length )
            {
                bool plateContentsMatchesRecipes = true;
                Debug.Log("same num of ingredients");

                for (int j = 0; j < waitingRecipeSO.kitchenObjectsArr.Length; j++)
                {
                    //going through all ingredients in the recipe
                    bool ingredientFound = false;
                    for (int k = 0; k < plateKitchenObject.GetKitchenObjectsArr().Length; k++)
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

                    //only way to reference a non static method (and this cant be using static context)
                    OnRecipeCompleted?.Invoke(this, new EventArgs());
                    Debug.Log("recipe succesful");
                    
             

                    return;
                }
            }
        }

        //no matches found - did not deliver correct recipe
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
