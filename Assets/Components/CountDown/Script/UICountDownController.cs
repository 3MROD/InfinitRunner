using System;
using TMPro;
using UnityEngine;

public class UICountDownController : MonoBehaviour
{
    // in canvas make countdown window and as child text
    [SerializeField] private GameObject _window;
    [SerializeField] private TMP_Text _countdownText;
    
    private bool _inCountdown;
    private CountdownState _countdownState;
    
    private void Awake()
    {
        // desactivate Window and listen to StateChange
        _window.SetActive(false);
        EventSystem.OnStateChanged += HandleStateChanged;
    }
    
    private void OnDestroy()
    {
        EventSystem.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(State state)
    {
        //When State change is called if in Countdown state Activate Window and make var _iscountdown true 
        if (state is not CountdownState countdownState)
        {
            _inCountdown = false;
            _window.SetActive(false);
            return;
        }

        _window.SetActive(true);
        _countdownState = countdownState;
        _inCountdown = true;
    }

    private void Update()
    {
        // if _isCountdown true Show the CountDowState Timer
        if (!_inCountdown)
        {
            return;
        }

        _countdownText.text = _countdownState.Timer.ToString("0");
    }
}