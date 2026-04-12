using System;
using UnityEngine;

public class ShipStateController : MonoBehaviour
{
    private ShipStateMachine _shipStateMachine;
    //To put in the Game on empty ShipStateController child of the GameController 

    private void Start()
    {
        //while create the StateMachine and set it to start as ShipIdleState
        _shipStateMachine = new ShipStateMachine();
        var initialStateShip = new ShipIdleState(_shipStateMachine);
        
        _shipStateMachine.ShipChangeState(initialStateShip);
    }

    private void Update()
    {
        _shipStateMachine.Update();
    }
}
