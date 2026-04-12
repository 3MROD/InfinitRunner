 using UnityEngine;

 public class GameOverState : State
 //end of game state call GameOver Event
    {
        public GameOverState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Game Over Enter");
            EventSystem.GameOver?.Invoke();
        }
        
        public override void Update()
        {
        }

        public override void Exit()
        {
            Debug.Log("Game Over Exit");
        }
    }
