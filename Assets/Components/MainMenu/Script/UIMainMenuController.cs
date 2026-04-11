using Components.SaveService;
using TMPro;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _runCountText;
    [SerializeField] private TMP_Text _lucioleCountText;

    private SaveData _saveData;
    
    private void Start()
    {
        var saveData = SaveService.Load();
        _saveData = saveData ?? new SaveData();
        
        _runCountText.text = "Runs: " + _saveData.RunCount;
        _lucioleCountText.text = "Lucioles: " + _saveData.LucioleCount;
    }

    public void StartGame()
    {
        _saveData.RunCount++;
        SaveService.Save(_saveData);
        
        SceneLoaderService.LoadGame();
    }
    
    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}