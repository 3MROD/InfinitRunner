

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
  
  
 
}
