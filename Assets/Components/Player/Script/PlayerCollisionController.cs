 using System;
 using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
 [Header("Parametres")] 
 [SerializeField] private Vector3 _sphereCenter;
 [SerializeField] private float _sphereRadius;
 private bool _isHit;

 private Vector3 _playerSpherePosition => transform.position + _sphereCenter;

 private void Start()
 {
  //EventSystem.OnPlayerSlideDown += ShrinkColliders();
 }

 private void Update()
 {
  Collider[] hitColliders = Physics.OverlapSphere(_playerSpherePosition, _sphereRadius);
  if (hitColliders.Length > 0 && !_isHit)
  {
   Debug.Log("Player hit something");
   _isHit = true;
  }

  if (hitColliders.Length == 0 )
  {
   _isHit = false;
  }
 }

 public void ShrinkColliders(bool isSlidingDown)
 {
  
 }
 private void OnDrawGizmos()
  {
   Gizmos.color = Color.red;
   Gizmos.DrawWireSphere(_playerSpherePosition, _sphereRadius);
   
  }
 }



