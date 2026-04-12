 using System;
 using System.Collections;
 using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{ 
 // controll the reaction to collision and life update depending is it's In MegaCharge / SlidingDown  and the Tags
 [SerializeField]private bool _inMegaCharge;
 [SerializeField]private bool _isSlidingDown;
 [SerializeField] private ParticleSystem _particleSystem;


 private void Start()
 {
  // listen to MegaCharge and SlideDown 
  EventSystem.MegaCharge += HandleMegaCharge;
  EventSystem.OnPlayerSlideDown += HandleOnPlayerSlideDown;
 // set var of each to false at start
  _inMegaCharge = false;
  _isSlidingDown = false;

 }

 private void HandleOnPlayerSlideDown(bool down)
 {
  //link OnPlyerSlideDown bool to the _islidingdown var
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
  // link the MegaCharge bool to the _isInMegaCharge var
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
  // if luciole add count
  if (other.CompareTag("Luciole"))
  {
   EventSystem.OnLucioleCollision?.Invoke();
   Destroy(other.gameObject);
   return;
  }
  // if Cloche add Life
  if (other.CompareTag("Cloche"))
  {
   EventSystem.Cloche?.Invoke();
   Destroy(other.gameObject);
   return;

  }
  // while in MegaCharge no Collision 
  if (_inMegaCharge)
  {
   Debug.Log("in mega charge");
   return;
  }
  // if the Tag is Encornable and you are charging you destroy game object otherwise you lose life
  if (other.CompareTag("Encornable"))
  {
   if (_isSlidingDown == false)
   {
    EventSystem.OnPlayerCollision?.Invoke();

    Debug.Log("Player hit Encornable");
    return;
   }
   Destroy(other.gameObject);
   _particleSystem.Play();
   Debug.Log("Skipped Encornable");
   return;
  }
  //if Tag is Bouse and you are charging nothing will happend otherwise you will hit and lose life

  if (other.CompareTag("Bouse"))
  {
   if (_isSlidingDown == false)
   {
    EventSystem.OnPlayerCollision?.Invoke();
    
    Debug.Log("Player hit bousier");
    return;
   }
   _particleSystem.Play();
   Debug.Log("skiped bousier");
   return;
  }
  // all other obstacle collided will take life away and player while flash
  EventSystem.OnPlayerCollision?.Invoke();
  EventSystem.Flash?.Invoke();
  Debug.Log("Player hit something");
 }


}



