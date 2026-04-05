using UnityEngine;

public class GameState : State

    {
    public GameState(StateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        Debug.Log("Game Started");
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdated;
    }
    public override void Update()
    {
    }

    public override void Exit()
    {
        Debug.Log("Game Exit");
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdated;

    }
    private void HandlePlayerLifeUpdated(int playerLife)
    {
        if (playerLife > 0)
        {
            return;
        }
        var gameOverState = new GameOverState(StateMachine);
        StateMachine.ChangeState(gameOverState);
    }
}
