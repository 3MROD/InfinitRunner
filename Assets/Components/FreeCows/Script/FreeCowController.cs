using System;
using UnityEngine;

public class FreeCowController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _cowPrefab;
    [SerializeField] private int freeCowCount = 0 ;
    private int _currentFreeCowCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        freeCowCount = _currentFreeCowCount;
        EventSystem.FreeCow += HandlerFreeCow;
        EventSystem.FreeCowCount?.Invoke(_currentFreeCowCount);
        
    }

    private void OnDestroy()
    {
        EventSystem.FreeCow -= HandlerFreeCow;
    }

    private void HandlerFreeCow()
    {
        InstantiateCow();
        _currentFreeCowCount++;
        EventSystem.FreeCowCount?.Invoke(_currentFreeCowCount);
        
    }

    private void InstantiateCow()
    {
        Instantiate(_cowPrefab, _spawnPoint.position, _spawnPoint.rotation);

    }
   
}
