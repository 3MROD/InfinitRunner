using System;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _lifeCount = 3;
    private int _currentLifeCount;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentLifeCount = _lifeCount;
        EventSystem.OnPlayerLifeUpdate?.Invoke(_currentLifeCount);
        EventSystem.OnPlayerCollision += HandlePlayerCollision;
        EventSystem.Cloche += HandleCloche;
    }

    private void HandleCloche()
    {
        if (_currentLifeCount < 3)
        {
            _currentLifeCount++;
            EventSystem.OnPlayerLifeUpdate?.Invoke(_currentLifeCount);
        }
        
    }

    private void HandlePlayerCollision()
    {
        if (_currentLifeCount - 1 < 0)
        {
           //the player Is dead 
           return;
        }
        _currentLifeCount--;
        EventSystem.OnPlayerLifeUpdate?.Invoke(_currentLifeCount);
    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerCollision -= HandlePlayerCollision;
        EventSystem.Cloche -= HandleCloche;
    }

    // Update is called once per frame
    void Update()
    {
       
                
        
    }
}
