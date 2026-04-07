
using UnityEngine;

public abstract class ShipState
{
    protected Boss boss;
    protected ShipStateMachine stateMachine;
    public ShipStateMachine ShipStateMachine { get; set; }

    public ShipState(Boss boss, ShipStateMachine stateMachine)
    {
        this.boss = boss;
        this.stateMachine = stateMachine;
    }

    protected ShipState(ShipStateMachine shipStateMachine)
    {
        ShipStateMachine = shipStateMachine;
    }

   
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
