using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private string fileName;

    private int ID;
    private DateTime startTime;
    private DateTime endTime;
    private int numRecipesCompleted;
    private int numIncorrectRecipes;
    private int sessionScore;

    private Sessions[] sessionsArr;
    private int arrSize = 0;

    private void Start()
    {
        //listens to event for when game is in specific state
        gameManager.OnStateChanged += GameManager_OnStateChanged;

        ID = LoginManager.GetCurrentPlayerID();
        
    }

    private void Awake()
    {
        sessionsArr = new Sessions[100];
    }


    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {

        if (gameManager.IsGameWaitingToStart())
        {
            startTime = DateTime.Now;
        }

        if (gameManager.IsGameOver())
        {
            endTime = DateTime.Now;

            AddSession();
        }  
        
    }

    private void AddSession()
    {
        numRecipesCompleted = DeliveryManager.GetTotalRecipesCompeleted();
        numIncorrectRecipes = DeliveryManager.GetTotalRecipesIncorrect();
        sessionScore = PointsUI.GetTotalPoints();

        sessionsArr[arrSize] = new Sessions(ID, startTime, endTime, numRecipesCompleted, numIncorrectRecipes, sessionScore);
        arrSize++;

    

        FileHandler.SaveToJSON<Sessions>(sessionsArr, fileName);
    }
}
