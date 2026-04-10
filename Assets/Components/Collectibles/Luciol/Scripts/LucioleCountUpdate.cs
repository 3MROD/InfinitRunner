using System;
using UnityEngine;

public class LucioleCountUpdate : MonoBehaviour
{
    [SerializeField] private int _LucioleCount = 1;
    private int _currentLucioleCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentLucioleCount = _LucioleCount;
        EventSystem.LucioleUpdate?.Invoke(_currentLucioleCount);
        EventSystem.OnLucioleCollision += HandleLucioleCollision;
        
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
    }
}
