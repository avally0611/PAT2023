using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI outputHighScore;
    [SerializeField] private TextMeshProUGUI outputNumRecDelivered;
    [SerializeField] private TextMeshProUGUI outputNumIncorrectRecipes;
    [SerializeField] private TextMeshProUGUI outputNumHoursPlayed;
    [SerializeField] private Button homeButton;

    private void Awake()
    {
        homeButton.onClick.AddListener(HomeClick);
    }


    public void Update()
    {
        if (StatsManager.GetStatsCalcBool())
        {

            outputHighScore.text = StatsManager.GetHighScore().ToString();
            outputNumRecDelivered.text = StatsManager.GetNumOfRecipesCompleted().ToString();
            outputNumIncorrectRecipes.text = StatsManager.GetNumOfIncorrectRecipes().ToString();
            outputNumHoursPlayed.text = StatsManager.GetMinutesPlayed().ToString() + " minutes";
        }
    }

    private void HomeClick()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

}
