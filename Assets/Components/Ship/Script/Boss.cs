using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Boss Life Controller
    [SerializeField] private int _bossLifeCount = 30;
    private int _currentBossLifeCount;
    private bool _resetBossLife;
    
    private void Start()
    {
        // set boss life and update OnShipLifeUpdate
        _currentBossLifeCount = _bossLifeCount;
        EventSystem.OnShipLifeUpdate?.Invoke(_currentBossLifeCount);
        // listen to OnShipCollision and OnshipStateChange
        EventSystem.OnShipCollision += HandleShipCollision;
        EventSystem.OnShipStateChange += ResetLife;
        Debug.Log(_currentBossLifeCount);
    }

    private void ResetLife(ShipState shipState)
    {
        // in ShipIdleState set current boss life full again and udpade OnShipLifeUpdate
        if (shipState is ShipIdleState shipIdleState)
        {
           _currentBossLifeCount = _bossLifeCount;
           EventSystem.OnShipLifeUpdate?.Invoke(_currentBossLifeCount);
        }

    }


    private void HandleShipCollision()
    {
        //when ShipCollision is called if boss life over 0 decrease life and update OnShipLifeUpdate
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
        EventSystem.OnShipStateChange -= ResetLife;
        
    }
}
