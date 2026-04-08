 using System;
 using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{ 
 [SerializeField]private bool _inMegaCharge;
 [SerializeField]private bool _isSlidingDown;
 


 private void Start()
 {
  EventSystem.MegaCharge += HandleMegaCharge;
  EventSystem.OnPlayerSlideDown += HandleOnPlayerSlideDown;
  _inMegaCharge = false;
  _isSlidingDown = false;

 }

 private void HandleOnPlayerSlideDown(bool down)
 {
  if (down)
  {
   _isSlidingDown = true;
  }
  else
  {
   _isSlidingDown = false;
  }
 }

 private void HandleMegaCharge(bool megaCharge)
 {
  if (megaCharge)
  {
   _inMegaCharge = true;
  }
  else
  {
   _inMegaCharge = false;
  }
 }
 

 private void OnDestroy()
 {
  EventSystem.MegaCharge -= HandleMegaCharge;
  EventSystem.OnPlayerSlideDown -= HandleOnPlayerSlideDown;
 }

 private void OnTriggerEnter(Collider other)
 {
  if (_inMegaCharge)
  {
   Debug.Log("in mega charge");
   return;
  }
  if (!other.CompareTag("Bouse"))
  {
   EventSystem.OnPlayerCollision?.Invoke();
   Debug.Log("Player hit something");
   
  }

  if (other.CompareTag("Bouse"))
  {
   if (_isSlidingDown == false)
   {
    EventSystem.OnPlayerCollision?.Invoke();
    
    Debug.Log("Player hit bousier");
   }

   Debug.Log("skiped bousier");
  }
  
 }
 
 
 }



