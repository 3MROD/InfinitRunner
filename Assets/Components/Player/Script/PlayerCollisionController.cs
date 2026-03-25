 using System;
 using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
 [Header("Parametres")] 
 [SerializeField] private Vector3 _sphereCenter;
 [SerializeField] private float _sphereRadius;
 [SerializeField] private Vector3 _shrinkSphereCenter;
 [SerializeField] private float _shrinkSphereRadius;
 private bool _isHit;

 private Vector3 _currentSphereCenter;
 private float _currentSphereRadius;
 private Vector3 _playerSpherePosition => transform.position + _currentSphereCenter;

 private void Start()
 {
  _currentSphereCenter = _sphereCenter;
  _currentSphereRadius = _sphereRadius;
  EventSystem.OnPlayerSlideDown += ShrinkColliders;
 }

 private void OnDestroy()
 {
  EventSystem.OnPlayerSlideDown -= ShrinkColliders;
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
  if (isSlidingDown)
  {
   _currentSphereCenter = _shrinkSphereCenter;
   _currentSphereRadius = _shrinkSphereRadius;
  }
  else
  {
   _currentSphereCenter = _sphereCenter;
   _currentSphereRadius = _sphereRadius;
  }
 }
 private void OnDrawGizmos()
  {
   Gizmos.color = Color.red;
   Gizmos.DrawWireSphere(transform.position +_sphereCenter, _sphereRadius);
   
   Gizmos.color = Color.green;
   Gizmos.DrawWireSphere(transform.position +_shrinkSphereCenter, _shrinkSphereRadius);
   Gizmos.color = Color.yellow;
   Gizmos.DrawWireSphere(_playerSpherePosition, _currentSphereRadius);
  }
 }



