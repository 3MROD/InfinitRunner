using UnityEngine;

public class GameStateController: MonoBehaviour
{
    //To put in the Game on empty StateMachineController child of the GameController 
    private StateMachine _stateMachine;
    
    private void Start()
    {
        //create first State as CountDown 
        _stateMachine = new StateMachine();
        var initialState = new CountdownState(_stateMachine);
        
        _stateMachine.ChangeState(initialState);
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }
}