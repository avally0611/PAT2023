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

    private void Awake()
    {
        playButton.onClick.AddListener(PlayClick);
        signoutButton.onClick.AddListener(SignoutClick);
        statsButton.onClick.AddListener(StatsClick);
   
        
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


}
