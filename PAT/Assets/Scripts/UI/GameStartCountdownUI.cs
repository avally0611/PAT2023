using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(gameManager.GetCountdownToStartTimer()).ToString();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (gameManager.IsCountDownToStartActive())
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
