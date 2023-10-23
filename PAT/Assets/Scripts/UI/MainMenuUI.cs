using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button signoutButton;
    [SerializeField] private Button statsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject controlsUI;

    private void Start()
    {
        controlsUI.SetActive(false);
    }

    private void Awake()
    {
        playButton.onClick.AddListener(PlayClick);
        signoutButton.onClick.AddListener(SignoutClick);
        statsButton.onClick.AddListener(StatsClick);
        controlsButton.onClick.AddListener(Show);
        quitButton.onClick.AddListener(Hide);


        Time.timeScale = 1f;
    }

    private void PlayClick()
    {
        
        Loader.Load(Loader.Scene.GameScene);
    }
    private void SignoutClick()
    {
        Loader.Load(Loader.Scene.LoginScene);
    }

    private void StatsClick()
    {
        Loader.Load(Loader.Scene.StatsScene);
    }

    private void Show()
    {
        controlsUI.SetActive(true);
    }

    private void Hide()
    {
        controlsUI.SetActive(false);
    }



}
