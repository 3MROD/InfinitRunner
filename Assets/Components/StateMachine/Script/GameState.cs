using System;
using UnityEngine;

public class GameState : State

    {
    public GameState(StateMachine stateMachine) : base(stateMachine) { }
    public int Timer => Mathf.RoundToInt(_timer);
    private float _timer;
    public override void Enter()
    {
        Debug.Log("Game Started");
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdated;
        
        _timer = 0f;
    }
    public override void Update()
    {
        _timer += Time.deltaTime;
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
