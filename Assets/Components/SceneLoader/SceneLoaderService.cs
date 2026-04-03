
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoaderService
{
    public static void LoadGame()
    {
        Debug.Log("Loading Game");
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        SceneManager.LoadScene("LevelUI", LoadSceneMode.Additive);
        Debug.Log("Game Loaded");
    }
    public static void LoadMainMenu ()
    {
        Debug.Log("Loading MainMenu...");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("MainMenu Loaded");
    }
}
