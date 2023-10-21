using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    //include switch that goes to achievements screen that shows recipes delivered, money and tips

    [SerializeField] GameManager gameManager;
    private void Start()
    {
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }


    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        
        if (gameManager.IsGameOver())
        {
            
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
