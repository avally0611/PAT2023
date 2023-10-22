using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesComp;
    [SerializeField] private TextMeshProUGUI incorrectRec;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Button homeButton;

    private void Start()
    {
        recipesComp.text = DeliveryManager.GetTotalRecipesCompeleted().ToString();
        incorrectRec.text = DeliveryManager.GetTotalRecipesIncorrect().ToString();
        score.text = PointsUI.GetTotalPoints().ToString();
    }

    private void Awake()
    {
        homeButton.onClick.AddListener(HomeClick);
    }

    private void HomeClick()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
