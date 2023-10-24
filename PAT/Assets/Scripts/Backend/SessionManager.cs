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

        if (gameManager.IsCountDownToStartActive())
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

        string startTimeString = startTime.ToString("HH:mm:ss");
        string endTimeString = endTime.ToString("HH:mm:ss");

        Sessions newSession = new Sessions(ID, startTimeString, endTimeString, numRecipesCompleted, numIncorrectRecipes, sessionScore);

        // Read existing player data from the JSON file
        Sessions[] existingSessions = FileHandler.ReadFromJSON<Sessions>(fileName);


        // Create a new array to hold the combined data
        Sessions[] combinedSessions;

        if (existingSessions != null)
        {
            // Append the new player to the existing data
            combinedSessions = new Sessions[existingSessions.Length + 1];
            existingSessions.CopyTo(combinedSessions, 0);
            combinedSessions[existingSessions.Length] = newSession;
        }
        else
        {
            // If there is no existing data, create a new array with just the new player
            combinedSessions = new Sessions[] { newSession };
        }

        // Save the combined data to the JSON file
        FileHandler.SaveToJSON<Sessions>(combinedSessions, fileName);


        
    }
}
