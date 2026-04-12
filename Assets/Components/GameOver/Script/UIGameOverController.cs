using System;
using UnityEngine;

public class UIGameOverController : MonoBehaviour
{
  //Ui window set active when the Game is over, main menu button fuction
  [SerializeField] private GameObject _gameOverScreen; 
  private void Awake()
  {
    // at Awake set to false and listent to OnStateChange
    _gameOverScreen.SetActive(false);
    EventSystem.OnStateChanged += HandleStateChanged;
  }

  private void HandleStateChanged(State newState)
  {
    // if the new state is GameOverState when StateChanged is Called set window as active
    _gameOverScreen.SetActive(newState is  GameOverState);
  }


  
  public void LoadMainMenu()
  {
    //for the GameOver MainMenu button that is a child of the window to access the SceneLoaderService to load the main menu
    SceneLoaderService.LoadMainMenu();
  }

  private void OnDestroy()
  {
    EventSystem.OnStateChanged -= HandleStateChanged;
  }
}
