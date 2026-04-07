

using UnityEngine;

public class ShipStateMachine
{
  public ShipState CurrentShipState;
 public void ShipChangeState(ShipState newState)
 {
  Debug.Log(" SHIP Changing state from: " + CurrentShipState?.GetType().Name + "to: " + newState.GetType().Name);
        
  CurrentShipState?.Exit();
  CurrentShipState = newState;
  CurrentShipState.Enter();
        
  EventSystem.OnShipStateChange?.Invoke(CurrentShipState);
 } 
    
 public void Update()
 {
  CurrentShipState?.Update();
 }
  
  
  
  
  
  
  
  
  
  
  //public ShipState currentShipState;


  //public void Initialize(ShipState startingState)
 // {
   // currentShipState = startingState;
    //currentShipState.EnterState();
  //}

  //public void ChangeState(ShipState newState)
  //{
 //   currentShipState.ExitState();
 //   currentShipState = newState;
  //  currentShipState.EnterState();
 // }
}
