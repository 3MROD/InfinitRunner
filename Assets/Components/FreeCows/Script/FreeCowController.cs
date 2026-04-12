using System;
using UnityEngine;

public class FreeCowController : MonoBehaviour
{
    // When Ship Looses all Life a freecow prefab is instantiated in Ship Spawn point and the freecowcount increase
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _cowPrefab;
    [SerializeField] private int freeCowCount = 0 ;
    private int _currentFreeCowCount;
    void Start()
    {
        //Listen, set and Update the freeCowCount
        _currentFreeCowCount = freeCowCount;
        EventSystem.FreeCow += HandlerFreeCow;
        EventSystem.FreeCowCount?.Invoke(_currentFreeCowCount);
      
        
        
    }

    private void OnDestroy()
    {
        EventSystem.FreeCow -= HandlerFreeCow;
    }

    private void HandlerFreeCow()
    {
        //When FreeCow called Instantiate Free Cow and increase the current count var then update the FreeCowCount int  
        InstantiateCow();
        _currentFreeCowCount++;
        EventSystem.FreeCowCount?.Invoke(_currentFreeCowCount);
        
    }

    private void InstantiateCow()
    {
        //instantiate freecow prefab at Ship SpawnPoint
        Instantiate(_cowPrefab, _spawnPoint.position, _spawnPoint.rotation);

    }
   
}
