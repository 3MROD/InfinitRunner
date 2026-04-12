using System;
using UnityEngine;

public class BouseMove : MonoBehaviour
{
    
    [SerializeField] private Transform _targetArrival;
    [SerializeField] private float _speed = 50f;

    //When  Bouse Projectil Prefab is instantiated it will traval towards the Ship, (ship has a Box Collider to destroy it on trigger) 
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, _targetArrival.position,_speed * Time.deltaTime);
        
       
    }

   
}
