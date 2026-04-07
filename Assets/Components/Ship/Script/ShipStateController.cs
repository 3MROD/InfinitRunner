using System;
using UnityEngine;

public class ShipStateController : MonoBehaviour
{
    private ShipStateMachine _shipStateMachine;

    private void Start()
    {
        _shipStateMachine = new ShipStateMachine();
        var initialStateShip = new ShipIdleState(_shipStateMachine);
        
        _shipStateMachine.ShipChangeState(initialStateShip);
    }

    private void Update()
    {
        _shipStateMachine.Update();
    }
}
