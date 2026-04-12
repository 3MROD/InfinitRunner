using UnityEngine;

public class StateMachine
{
    //StateMachine to deal with the Changing of states and update the CurrentState
    public State CurrentState;
    
    public void ChangeState(State newState)
    {
        Debug.Log("Changing state from: " + CurrentState?.GetType().Name + "to: " + newState.GetType().Name);
        
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
        
        EventSystem.OnStateChanged?.Invoke(CurrentState);
    }
    
    public void Update()
    {
        CurrentState?.Update();
    }
}