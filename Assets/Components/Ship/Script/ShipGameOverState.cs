using UnityEngine;

public class ShipGameOverState : ShipState
{
    //just to stop ShipAttack cycle
    public ShipGameOverState(Boss boss, ShipStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public ShipGameOverState(ShipStateMachine shipStateMachine) : base(shipStateMachine)
    {
    }

    public override void Enter()
    {
      Debug.Log("Entering ShipGameOverState");
    }

    public override void Update()
    {
       
    }

    public override void Exit()
    {
       
    }
}
