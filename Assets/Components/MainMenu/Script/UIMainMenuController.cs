using System;
using Components.SaveService;
using TMPro;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    //Main menu UI , display Bestime, Run count and Luciole count, save new run count
    [SerializeField] private TMP_Text _runCountText;
    [SerializeField] private TMP_Text _lucioleCountText;
    [SerializeField] private TMP_Text _bestTimeText;

    private SaveData _saveData;
    
    private void Start()
    {
        // load the save Data 
        _saveData = SaveService.Load();
        // runcount text display of save
        _runCountText.text = "Attempts: " + _saveData.RunCount;
        //best time text display of save

        if (_saveData.BestTime == 0)
        {
            _bestTimeText.text = "No Best Time";
        }
        else
        {
            var timeSpan = new TimeSpan(0, 0, _saveData.BestTime);
            _bestTimeText.text = "Best Time: " + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
        }
        //luciole text display of save
        _lucioleCountText.text = "Lucioles: " + _saveData.LucioleCount;
 
       
    }

    public void StartGame()
    {
        // for the start button that is child to acces SceneLoaderService and Load Game, increase runcount and save
        _saveData.RunCount++;
        SaveService.Save(_saveData);
        
        SceneLoaderService.LoadGame();
    }
    
    public void QuitGame()
    {
        // for the Quit Button that is a child if not builded quit with the UnityEditor
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}