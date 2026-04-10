using System;
using System.Collections.Generic;
using UnityEngine;

public class BouseAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnDeparture;
    [SerializeField] private GameObject _bouse;

    private GameObject  _newBouse;
    private bool _isSlidingDown;

    private void Start()
    {
        EventSystem.OnPlayerSlideDown += HandlePlayerSlideDawn;
    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerSlideDown -= HandlePlayerSlideDawn;
    }

    private void HandlePlayerSlideDawn(bool playerSlideDown)
    {
        _isSlidingDown= playerSlideDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bouse") && _isSlidingDown )
        {
            
            Debug.Log("toucher");
            InstanciateBouse();
            Destroy(other.gameObject);
        }
    }
    
    private void InstanciateBouse()
    {
        _newBouse = Instantiate(_bouse, _spawnDeparture.position, _spawnDeparture.rotation);
    }

   
}
