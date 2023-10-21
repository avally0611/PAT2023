using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton ;
    [SerializeField] private Button optionsButton;
    [SerializeField] OptionsUI optionsUI;

    private void Awake()
    {
        resumeButton.onClick.AddListener(resumeClick);
        quitButton.onClick.AddListener(QuitClick);
        optionsButton.onClick.AddListener(optionsUI.ShowOptionsUI);
    }

    private void resumeClick()
    {
        gameManager.TogglePauseGame();
    }
    private void QuitClick()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameManager.OnGamePaused += GameManager_OnGamePaused;
        gameManager.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }
}
