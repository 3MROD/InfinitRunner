using System;
using TMPro;
using UnityEngine;

public class UITimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    private GameState _gameState;

    private bool _inGameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        EventSystem.OnStateChanged += HandleStateChanged;
        _timeText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventSystem.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(State newState)
    {
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
        if (!_inGameState)
        {
            return;
        }

        var timeSpan = new TimeSpan(0, 0, _gameState.Timer);
        _timeText.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
    }
}
