using System;
using Components.SaveService;
using UnityEngine;

public class LucioleCountUpdate : MonoBehaviour
{
    [SerializeField] private int _LucioleCount = 1;
    private int _currentLucioleCount;
    private SaveData _saveData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var saveData = SaveService.Load();
        _saveData = saveData ?? new SaveData();
        _currentLucioleCount = _LucioleCount + _saveData.LucioleCount;
        EventSystem.LucioleUpdate?.Invoke(_currentLucioleCount);
        EventSystem.OnLucioleCollision += HandleLucioleCollision;
        EventSystem.OnStateChanged += HandleGameOver;

    }

    private void HandleGameOver(State state)
    {
        if (state is GameOverState gameOverState)
        {
             _saveData.LucioleCount = _currentLucioleCount;
            SaveService.Save(_saveData);
        }
    }

    private void HandleLucioleCollision()
    {
        _currentLucioleCount++;
        EventSystem.LucioleUpdate?.Invoke(_currentLucioleCount);
        Debug.Log(_currentLucioleCount);
    }

   

    private void OnDestroy()
    {
        EventSystem.OnLucioleCollision-= HandleLucioleCollision;
        EventSystem.OnStateChanged -= HandleGameOver;
    }
}
