using System;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    //Amount of Life and Life Updated
    [SerializeField] private int _lifeCount = 3;
    private int _currentLifeCount;
    
    void Start()
    {
        //Set Life count
        _currentLifeCount = _lifeCount;
        // update LifeCount to OnPlayerLifeUpdate Int
        EventSystem.OnPlayerLifeUpdate?.Invoke(_currentLifeCount);
        // listen to OnplayerCollision and Cloche 
        EventSystem.OnPlayerCollision += HandlePlayerCollision;
        EventSystem.Cloche += HandleCloche;
    }

    private void HandleCloche()
    {
        // If Cloche called and the currents life is not at 3 add one life, and update OnplayerLifeUpdate 
        if (_currentLifeCount < 3)
        {
            _currentLifeCount++;
            EventSystem.OnPlayerLifeUpdate?.Invoke(_currentLifeCount);
        }
        
    }

    private void HandlePlayerCollision()
    {
        // when on collision called if the playr has more than 0 life decrease life and update OnplayerLifeUpdate
        if (_currentLifeCount - 1 < 0)
        {
           //the player Is dead 
           return;
        }
        //decrease life
        _currentLifeCount--;
        EventSystem.OnPlayerLifeUpdate?.Invoke(_currentLifeCount);
    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerCollision -= HandlePlayerCollision;
        EventSystem.Cloche -= HandleCloche;
    }

   
}
