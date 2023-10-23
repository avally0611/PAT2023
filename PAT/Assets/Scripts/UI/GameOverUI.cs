using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    

    [SerializeField] GameManager gameManager;
    private void Start()
    {
        //listens to event for when game is in "Game Over" state
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }


    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        
        if (gameManager.IsGameOver())
        {
            
            Show();

            //this is to delay the loading of next scene so Game Over sign is showed (When using Invoke =, you parse method as string)
            Invoke("LoadResultsScene", 5f);
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

    private void LoadResultsScene()
    {
        Loader.Load(Loader.Scene.ResultsScene);
    }

}
