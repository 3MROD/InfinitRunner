using UnityEngine;

public class ShipAttackState : ShipState
{
    public ShipAttackState(Boss boss, ShipStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

   

    public ShipAttackState(ShipStateMachine shipStateMachine) : base(shipStateMachine)
    { }

    public override void Enter()
    {
       Debug.Log("ship attack mode");
    }

    public override void Update()
    {
       Debug.Log("attacked");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
