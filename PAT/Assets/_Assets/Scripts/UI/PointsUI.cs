using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    private void UpdateVisual(int num)
    {
        pointsText.text = num.ToString(); 
    }

    public void CalculatePoints(RecipeSO recipeSO, TimeSpan duration)
    {
        int totalPoints = 0;

        double secondsDifference = duration.TotalSeconds;

        String inputRecipeName = recipeSO.recipeName;

        if (inputRecipeName.Equals("Burger"))
        {
            if (secondsDifference > 7 && secondsDifference <= 10)
            {
                totalPoints = 20;
            }
            else if (secondsDifference > 5 && secondsDifference <= 7)
            {
                totalPoints = 23;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 5)
            {
                totalPoints = 25;
            }
        }
        else if (inputRecipeName.Equals("Mega Burger"))
        {
            if (secondsDifference > 13 && secondsDifference <= 17)
            {
                totalPoints = 27;
            }
            else if (secondsDifference > 10 && secondsDifference <= 13)
            {
                totalPoints = 30;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 10)
            {
                totalPoints = 34;
            }
        }
        else if (inputRecipeName.Equals("Cheese Burger"))
        {
            if (secondsDifference > 10 && secondsDifference <= 13)
            {
                totalPoints = 22;
            }
            else if (secondsDifference > 7 && secondsDifference <= 10)
            {
                totalPoints = 25;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 7)
            {
                totalPoints = 28;
            }
        }
        else if (inputRecipeName.Equals("Salad"))
        {
            if (secondsDifference > 4 && secondsDifference <= 7)
            {
                totalPoints = 10;
            }
            else if (secondsDifference > 3 && secondsDifference <= 4)
            {
                totalPoints = 11;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 3)
            {
                totalPoints = 17;
            }


        }

        UpdateVisual(totalPoints);

        
    }
}
