
using UnityEngine;
using UnityEngine.SceneManagement;
//Scenes to be accessible as " SceneManagement.**** (**** = LoadGame or LoadMainMEnu ),ex: used for buttons 
public static class SceneLoaderService
{
    // to access different Scenes 
    public static void LoadGame()
    {
        // Load Game scene with UI over it
        Debug.Log("Loading Game");
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        SceneManager.LoadScene("LevelUI", LoadSceneMode.Additive);
        Debug.Log("Game Loaded");
    }
    public static void LoadMainMenu ()
    {
        // Load Main Menu scene
        Debug.Log("Loading MainMenu...");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("MainMenu Loaded");
    }
}
