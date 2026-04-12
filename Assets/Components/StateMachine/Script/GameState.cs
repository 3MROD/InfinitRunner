using System;
using Components.SaveService;
using UnityEngine;

public class GameState : State

    {
    public GameState(StateMachine stateMachine) : base(stateMachine) { }
    public int Timer => Mathf.RoundToInt(_timer);
    private float _timer;
    public override void Enter()
    {
       // set timer 0 and listen ton OnPlayerLife Update
        Debug.Log("Game Started");
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdated;
        _timer = 0f;
    }
    public override void Update()
    {
        //time increase in time
        _timer += Time.deltaTime;
    }

    public override void Exit()
    {
        //When GameState Over Load and compare best time and save if higher 
        var saveData = SaveService.Load();
        if (saveData.BestTime < Timer)
        {
            saveData.BestTime = Timer;
            SaveService.Save(saveData);
        }

        Debug.Log("Game Exit");
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdated;
        

    }
    private void HandlePlayerLifeUpdated(int playerLife)
    {
        //if player life is 0 new state to GameOverState
        if (playerLife > 0)
        {
            return;
        }
        var gameOverState = new GameOverState(StateMachine);
        StateMachine.ChangeState(gameOverState);
       
    }
    
    }
