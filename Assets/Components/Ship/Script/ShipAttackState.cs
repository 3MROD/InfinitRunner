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
       EventSystem.OnShipLifeUpdate += HandleShipLifeUpdate;
    }

    private void HandleShipLifeUpdate(int shipLife)
    {
        if (shipLife > 0)
        {
            return;
        }
        EventSystem.FreeCow?.Invoke();
        var shipIdleState = new ShipIdleState(ShipStateMachine);
        ShipStateMachine.ShipChangeState(shipIdleState);
    }

    public override void Update()
    {
      
       
    }

    public override void Exit()
    {
        Debug.Log("End of Attack");
        EventSystem.OnShipLifeUpdate -= HandleShipLifeUpdate;
        
    }
}
