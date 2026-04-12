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
        //Listen to OnShipLifeUpdate and GameOver
       Debug.Log("ship attack mode");
       EventSystem.OnShipLifeUpdate += HandleShipLifeUpdate;
       EventSystem.GameOver += HandleGameOver;
    }

    private void HandleShipLifeUpdate(int shipLife)
    {
        //if ship life under 0 Change State to ShipIdleState and Invoke FreeCow
        if (shipLife > 0)
        {
            return;
        }
        EventSystem.FreeCow?.Invoke();
        var shipIdleState = new ShipIdleState(ShipStateMachine);
        ShipStateMachine.ShipChangeState(shipIdleState);
    }
    private void HandleGameOver()
    {
        //if GameOver set ShipState to ShipGameOverState to stop the ShipAttack cycle
        var shipGameOverState = new ShipGameOverState(ShipStateMachine);
        ShipStateMachine.ShipChangeState(shipGameOverState);
    }

    public override void Update()
    {
      
       
    }

    public override void Exit()
    {
        Debug.Log("End of Attack");
        EventSystem.OnShipLifeUpdate -= HandleShipLifeUpdate;
        EventSystem.GameOver -= HandleGameOver;
        
    }
}
