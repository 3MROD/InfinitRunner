using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        #if !UNITY_EDITOR
        Application.Quit();
        #else
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        
        
    }

    public void StarGame()
    {
        SceneLoaderService.LoadGame();
    }
}
