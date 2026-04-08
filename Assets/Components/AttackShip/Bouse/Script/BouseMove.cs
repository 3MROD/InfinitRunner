using System;
using UnityEngine;

public class BouseMove : MonoBehaviour
{
    
    [SerializeField] private Transform _targetArrival;
    [SerializeField] private float _speed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, _targetArrival.position,_speed * Time.deltaTime);
        
       
    }

   
}
