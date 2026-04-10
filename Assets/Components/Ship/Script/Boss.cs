using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int _bossLifeCount = 30;
    private int _currentBossLifeCount;
    
    private void Start()
    {
        _currentBossLifeCount = _bossLifeCount;
        EventSystem.OnShipLifeUpdate?.Invoke(_currentBossLifeCount);
        EventSystem.OnShipCollision += HandleShipCollision;
        Debug.Log(_currentBossLifeCount);
    }

  
    private void HandleShipCollision()
    {
        if (_currentBossLifeCount - 1 < 0)
        {
            
            return;
        }
        _currentBossLifeCount--;
        EventSystem.OnShipLifeUpdate?.Invoke(_currentBossLifeCount);
        Debug.Log(_currentBossLifeCount);
    }
    

    private void OnDestroy()
    {
        EventSystem.OnShipCollision -= HandleShipCollision;
        
    }
}
