using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private static int highscore;
    private static int totalRecipesCompl;
    private static int numIncorrectRecipes;
    private static double totalMinutesPlayed;
    private int ID;
    private static bool statsCalc = false;

    private Sessions[] sessionsArr = new Sessions[100];

    private void Start()
    {
        
        ID = LoginManager.GetCurrentPlayerID();
        GetFileData();
        statsCalc = true;
    }

    private void GetFileData()
    {
        highscore = 0;
        totalRecipesCompl = 0;
        numIncorrectRecipes = 0;
        totalMinutesPlayed = 0.0;

        sessionsArr = FileHandler.ReadFromJSON<Sessions>(fileName);

        if (sessionsArr != null)
        {
            //I dont use arrCount here because this is the array that we receive from the JSON files
            for (int i = 0; i < sessionsArr.Length; i++)
            {
                if (sessionsArr[i].ID == ID)
                {
                    highscore += sessionsArr[i].sessionScore;
                    totalRecipesCompl += sessionsArr[i].numRecipesCompleted;
                    
                    numIncorrectRecipes += sessionsArr[i].numRecipesCompleted;
                    totalMinutesPlayed += (DateTime.Parse(sessionsArr[i].endTime) - DateTime.Parse(sessionsArr[i].startTime)).TotalMinutes;
                }
            }

        }
    }

    public static int GetHighScore()
    {
        return highscore;
    }

    public static int GetNumOfRecipesCompleted()
    {
        
        return totalRecipesCompl;
    }

    public static int GetNumOfIncorrectRecipes()
    {
        return numIncorrectRecipes;
    }

    public static double GetMinutesPlayed()
    {
        double rounded = Math.Round(totalMinutesPlayed, 0);
        return rounded;
    }

    public static bool GetStatsCalcBool()
    {
        return statsCalc;
    }
}
