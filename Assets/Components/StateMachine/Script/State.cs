public abstract class State
{
    // what Will be injected in all different States that heritate from this class
    protected readonly StateMachine StateMachine;
    
    protected State(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }
    
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
