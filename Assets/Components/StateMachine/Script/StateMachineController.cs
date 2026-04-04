  using System;
  using UnityEngine;

  public class StateMachineController : MonoBehaviour
  {
      private StateMachine _stateMachine;
        private void Awake()
        {
           _stateMachine = new StateMachine();
           var initialState = new CountdownState(_stateMachine);
           _stateMachine.ChangeState(initialState);
           
        }

        private void Update()
        {
            _stateMachine.Update();
        }
  }
