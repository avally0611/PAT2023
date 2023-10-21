using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverCountDown;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        gameOverCountDown.text = Mathf.Ceil(gameManager.GetCountdownToGameOver()).ToString();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (gameManager.IsGamePlaying())
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
