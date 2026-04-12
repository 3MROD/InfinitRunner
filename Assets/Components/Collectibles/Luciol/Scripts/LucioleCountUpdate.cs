using System;
using Components.SaveService;
using UnityEngine;

public class LucioleCountUpdate : MonoBehaviour
{
    [SerializeField] private int _LucioleCount = 1;
    [SerializeField] private AudioSource _audioSource;
    private int _currentLucioleCount;
    private SaveData _saveData;
    // 
    void Start()
    {
        // get the amount of luioles From the last save
        var saveData = SaveService.Load();
        _saveData = saveData ?? new SaveData();
        _currentLucioleCount = _LucioleCount + _saveData.LucioleCount;
        //Update the luciole amount
        EventSystem.LucioleUpdate?.Invoke(_currentLucioleCount);
        // listen to Eventsysteme  
        EventSystem.OnLucioleCollision += HandleLucioleCollision;
        EventSystem.OnStateChanged += HandleGameOver;

    }

    private void HandleGameOver(State state)
    {
        // When state changed is Called and it's GameOver update Save data to the new LucioleCount for the next round
        if (state is GameOverState gameOverState)
        {
             _saveData.LucioleCount = _currentLucioleCount;
            SaveService.Save(_saveData);
        }
    }

    private void HandleLucioleCollision()
    {
        // when OnLucioleCollision is called increase the Luciole count by one and update the LucioleUpdate int, play sfx 
        _currentLucioleCount++;
        _audioSource.Play();
        EventSystem.LucioleUpdate?.Invoke(_currentLucioleCount);
        Debug.Log(_currentLucioleCount);
    }

   

    private void OnDestroy()
    {
        EventSystem.OnLucioleCollision-= HandleLucioleCollision;
        EventSystem.OnStateChanged -= HandleGameOver;
    }
}
