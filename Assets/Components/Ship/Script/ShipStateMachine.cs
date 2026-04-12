

using UnityEngine;

public class ShipStateMachine
{
 //ShipStateMachine to deal with the Changing of states and update 
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
  
  
 
}
