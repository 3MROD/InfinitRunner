using System;
using TMPro;
using UnityEngine;

public class UITimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    private GameState _gameState;

    private bool _inGameState;

    void Awake()
    {
        //Listen to OnStateChanged, hide text
        EventSystem.OnStateChanged += HandleStateChanged;
        _timeText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventSystem.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(State newState)
    {
        // in GameState show the Time Text and set _inGameState var to true
        if (newState is not GameState gameState)
        {
            _inGameState = false;
            _timeText.gameObject.SetActive(false);
            return;
        }

        _gameState = gameState;
        _inGameState = true;
        _timeText.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        // when in gameState the text will be in 00:00 and from the GameStateTimer
        if (!_inGameState)
        {
            return;
        }

        var timeSpan = new TimeSpan(0, 0, _gameState.Timer);
        _timeText.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
    }
}
