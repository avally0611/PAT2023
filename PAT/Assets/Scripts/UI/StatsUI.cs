using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI outputHighScore;
    [SerializeField] private TextMeshProUGUI outputNumRecDelivered;
    [SerializeField] private TextMeshProUGUI outputNumIncorrectRecipes;
    [SerializeField] private TextMeshProUGUI outputNumHoursPlayed;

    public void Update()
    {
        if (StatsManager.GetStatsCalcBool())
        {

            outputHighScore.text = StatsManager.GetHighScore().ToString();
            outputNumRecDelivered.text = StatsManager.GetNumOfRecipesCompleted().ToString();
            outputNumIncorrectRecipes.text = StatsManager.GetNumOfIncorrectRecipes().ToString();
            outputNumHoursPlayed.text = StatsManager.GetTotalHoursPlayed().ToString() + " minutes";
        }
    }
}
