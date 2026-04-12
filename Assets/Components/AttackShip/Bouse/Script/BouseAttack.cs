using System;
using System.Collections.Generic;
using UnityEngine;

public class BouseAttack : MonoBehaviour
{
    [SerializeField ] private Transform _spawnDeparture;
    [SerializeField] private GameObject _bouse;

    private GameObject  _newBouse;
    private bool _isSlidingDown;
    
// This Script is on The Player Gameobject to detect Bousier GameObject and enable Attack by instanciating bouse projectil
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
        // slidding down bool
        _isSlidingDown= playerSlideDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        // We can Only instantiate a Bouse if we are slidding down 
        if (other.CompareTag("Bouse") && _isSlidingDown )
        {
            
            Debug.Log("toucher");
            InstanciateBouse();
            Destroy(other.gameObject);
        }
    }
    
    private void InstanciateBouse()
    {
        // the new bool will be Instantiate the bouse projectil prefab 
        // the Spawn point is a Empty child of the Player GameObject 
        _newBouse = Instantiate(_bouse, _spawnDeparture.position, _spawnDeparture.rotation);
    }

   
}
