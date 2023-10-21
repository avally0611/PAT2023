using System;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    static int totalPoints = 0;
    static bool pointsChanged = false;

    public void Update()
    {
        if (pointsChanged)
        {
            pointsText.text = totalPoints.ToString();
            pointsChanged = false;
        }
        
    }

    public static void CalculatePoints(RecipeSO recipeSO, TimeSpan duration)
    {
        int points = 0;

        double secondsDifference = duration.TotalSeconds;

        String inputRecipeName = recipeSO.recipeName;

        if (inputRecipeName.Equals("Burger"))
        {
            if (secondsDifference > 7 && secondsDifference <= 10)
            {
                points = 20;
            }
            else if (secondsDifference > 5 && secondsDifference <= 7)
            {
                points = 23;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 5)
            {
                points = 25;
            }
            else
            {
                points = 5;
            }

        }
        else if (inputRecipeName.Equals("Mega Burger"))
        {
            if (secondsDifference > 13 && secondsDifference <= 17)
            {
                points = 27;
            }
            else if (secondsDifference > 10 && secondsDifference <= 13)
            {
                points = 30;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 10)
            {
                points = 34;
            }
            else
            {
                points = 5;
            }
        }
        else if (inputRecipeName.Equals("Cheese Burger"))
        {
            if (secondsDifference > 10 && secondsDifference <= 13)
            {
                points = 22;
            }
            else if (secondsDifference > 7 && secondsDifference <= 10)
            {
                points = 25;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 7)
            {
                points = 28;
            }
            else
            {
                points = 5;
            }
        }
        else if (inputRecipeName.Equals("Salad"))
        {
            if (secondsDifference > 4 && secondsDifference <= 7)
            {
                points = 10;
            }
            else if (secondsDifference > 3 && secondsDifference <= 4)
            {
                points = 11;
            }
            else if (secondsDifference >= 1 && secondsDifference <= 3)
            {
                points = 17;
            }
            else
            {
                points = 5;
            }


        }

        totalPoints += points;
        pointsChanged = true;

        
    }

    public static void IncorrectRecipePoints()
    {
        totalPoints -= 5;
        pointsChanged = true;
    }

}
