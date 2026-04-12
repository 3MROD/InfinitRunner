using System;

using UnityEngine;

public class ShipIdleState : ShipState
{
    private float _initialTime = 15f;
    private float _timer;
    public float Timer => _timer;

    public ShipIdleState(Boss boss, ShipStateMachine stateMachine) : base(boss, stateMachine)
    {
    }

    public ShipIdleState(ShipStateMachine shipStateMachine) : base(shipStateMachine)
    {
    }

 

    public override void Enter()
    {
        //Set the Timer full and Listen to GameOver
        Debug.Log("Entering Ship State");
        _timer = _initialTime;
        EventSystem.GameOver += HandleGameOver;
    }

    private void HandleGameOver()
    {
        //Change state to ShipGameOverState when GameOver is Called
        var shipGameOverState = new ShipGameOverState(ShipStateMachine);
        ShipStateMachine.ShipChangeState(shipGameOverState);
    }

    public override void Update()
    {
        //Timer count 
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
            
        }
        
        // Go to Attack state
        var shipAttackState = new ShipAttackState(ShipStateMachine);
        ShipStateMachine.ShipChangeState(shipAttackState);
        
    }

    public override void Exit()
    {
        Debug.Log("Ship Countdown finished");
        EventSystem.GameOver -= HandleGameOver;
    }
}
