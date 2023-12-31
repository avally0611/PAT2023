using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//not attached to an object
//handles all the scene changing in the game 
public static class Loader 
{

    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
        StatsScene,
        LoginScene,
        ResultsScene,
    }

    private static Scene targetScene;

    //loads scene
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

        
    }

    //implements smooth loading - no stuuttering or stopping in between scenes
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
