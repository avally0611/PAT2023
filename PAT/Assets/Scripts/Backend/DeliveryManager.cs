using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;

//HANDLES LOGIC WITH REGARDS TO WAITING RECIPES AND DELIVERING RECIPES
public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] GameManager gameManager;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailure;

    //random recipes that need to be completed
    private static RecipeSO[] waitingRecipeSOArr;
    private static int waitingRecipeArrCount = 0;

    //array that stores start time of recipes - calculate points
    private static DateTime[] startTimeArr;
    private static int startTimeArrCount = 0;

    //timer to spawn recipes
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private int waitingRecipesMax = 3;

    //for results screen
    private static int totalRecipesCompleted = 0;
    private static int totalRecipesIncorrect = 0;

    private void Awake()
    {
        waitingRecipeSOArr = new RecipeSO[10];
        startTimeArr = new DateTime[10];
    }

    //checks if game on - start recipe spawning timer - adds recipe to waiting recipes array
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

                if (waitingRecipeArrCount < waitingRecipesMax)
                {
                    //Range is for floating numbers, Next is for integers
                    RecipeSO waitingRecipeSO = listOfRecipeSO[UnityEngine.Random.Range(0, listOfRecipeSO.Length)];

                    waitingRecipeSOArr[waitingRecipeArrCount] = waitingRecipeSO;
                    startTimeArr[startTimeArrCount] = DateTime.Now;

                    startTimeArrCount++;
                    waitingRecipeArrCount++;


                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }


            }
        }
        
    }

    //logic to check if any of waiting recipes match with delivered recipes - if so cue animations/remove waiitng recipe/points
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        //when order finished - points calculations
        DateTime placementTime = DateTime.Now;
        int foundIndex = -1;

        for (int i = 0; i < waitingRecipeArrCount; i++)
        {
            //go through each waiting recipe and see if ingredients match
            RecipeSO waitingRecipeSO = waitingRecipeSOArr[i];
            
            //found recipe
            foundIndex = i;

            //going through all ingredients in the recipe
            for (int j = 0; j < waitingRecipeSO.kitchenObjectsArr.Length; j++)
            {
                bool ingredientFound = false;

                //going through all ingredients on plate
                for (int k = 0; k < plateKitchenObject.GetArrCount(); k++)
                {
                    
                    //check if first ingreidens match
                    if (waitingRecipeSO.kitchenObjectsArr[j].Equals(plateKitchenObject.GetKitchenObjectsArr()[k]))
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
                    
                    foundIndex = -1;
                    //if you dont find one ingredient, then dont look at another
                    break;
                }
            }

            if (foundIndex >= 0)
            {
                //player delivered correct recipe

                totalRecipesCompleted++;

                TimeSpan cookTime = (placementTime - startTimeArr[foundIndex]);

                PointsUI.CalculatePoints(waitingRecipeSO, cookTime);

                Remove(foundIndex);

                OnRecipeCompleted?.Invoke(this, new EventArgs());
                    
                OnRecipeSuccess?.Invoke(this, new EventArgs());
                    

                return;
            }

            
        }
        //no matches found - did not deliver correct recipe
        PointsUI.IncorrectRecipePoints();

        totalRecipesIncorrect++;

        OnRecipeFailure?.Invoke(this, new EventArgs());


    }

    private void Remove(int index)
    {
        for (int i = index + 1; i < waitingRecipeArrCount; i++)
        {
            waitingRecipeSOArr[i - 1] = waitingRecipeSOArr[i];
            startTimeArr[i - 1] = startTimeArr[i];
        }

        waitingRecipeArrCount--;
        startTimeArrCount--;

    }

    //gets arrays of all recipes : Burger, cheese burger, salad, mega burger
    public RecipeSO[] GetWaitingRecipeSoArr()
    {
        return waitingRecipeSOArr;
    }

    //get arr size - used in UI implementaion
    public int GetArrCount()
    {
        return waitingRecipeArrCount;
    }

    //for results
    public static int GetTotalRecipesCompeleted()
    {
        return totalRecipesCompleted;
    }

    public static int GetTotalRecipesIncorrect()
    {
        return totalRecipesIncorrect;
    }

    


}
