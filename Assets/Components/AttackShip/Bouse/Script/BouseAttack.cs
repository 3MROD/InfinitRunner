using System;
using System.Collections.Generic;
using UnityEngine;

public class BouseAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnDeparture;
    [SerializeField] private GameObject _bouse;

    private GameObject  _newBouse;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("toucher");
        InstanciateBouse();
    }
    
    private void InstanciateBouse()
    {
        _newBouse = Instantiate(_bouse, _spawnDeparture.position, _spawnDeparture.rotation);
    }

   
}
