using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Tooltip("Translation speed of chunks in m/s")] private float _translationSpeed = 1f;
    [SerializeField] private int _activeChunkCount = 5;
    [SerializeField] private int _behindChunkCount = 1;
    [SerializeField] private float _stopDelayOnDammage = 0.2f;
    
    [Header("Components")]
    [SerializeField] private ChunkController[] _chunksPool;
    
    [Header("Over Time")]
    [SerializeField, Tooltip("Interval in seconds between each speed increase")] private float _speedUpInterval = 15f;
    [SerializeField, Tooltip("Speed increase applied on each interval")] private float _speedUpIncrease = 1.5f;
    
    private readonly List<ChunkController> _instancedChunks = new();
    private float _baseTranslationSpeed;
    
    private float _stopDelayTimer;
    private bool _stopped;
    private bool _inGameState;
    private bool _inShipAttackState;
    
    private GameState _gameState;
    private int _lastSpeedUpTime; // last time when SpeedUp was applied

    private ShipAttackState _shipAttackState;
    private void Awake()
    {
        EventSystem.OnStateChanged += HandleStateChanged;
        EventSystem.OnShipStateChange += HandleShipStateChanged; //allez s'abbonner au envent systeme qui dit si le Ship a changer de state
    }

  
    private void HandleShipStateChanged(ShipState newShipState)
    {
        if (newShipState is not ShipAttackState shipAttackState)
        {
            _inShipAttackState = false;
            return;
        }

        _shipAttackState = shipAttackState;
        _inShipAttackState = true;
        //change Chunk POOL ??
    }

    private void HandleStateChanged(State newState)
    {
        if (newState is not GameState gameState)
        {
            EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdate;
            _inGameState =  false;
            return;
        }
        _gameState = gameState;
        _translationSpeed = _baseTranslationSpeed;
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdate;
        _inGameState = true;

    }

    private void Start()
    {
        _baseTranslationSpeed = _translationSpeed;
        _translationSpeed = 0;
        AddBaseChunk();
    }

    private void OnDestroy()
    {
       EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdate;
       EventSystem.OnStateChanged -= HandleStateChanged;
    }

    private void HandlePlayerLifeUpdate(int playerLifeCount)
    {
        if (playerLifeCount > 0)
        {
            _stopped = true;
           // return;
        }

        _translationSpeed = 0;
    }

    private void Update()
    {
        if (!_inGameState)
        {
            return;
        }
        ResetMovementAfterDelay();
        TranslateChunks();

        UpdateChunks();
    }

    private void TranslateChunks()
    {
        var gameTimer = _gameState.Timer;
        if (gameTimer != 0 && gameTimer % _speedUpInterval == 0 && gameTimer != _lastSpeedUpTime)
        {
            Debug.Log("Speed Up!");
            _translationSpeed += _speedUpIncrease * Time.deltaTime;
            _baseTranslationSpeed = _translationSpeed;
            _lastSpeedUpTime = gameTimer;
        }
        ResetMovementAfterDelay();
        foreach (var chunk in _instancedChunks)
        {
            chunk.transform.Translate(Vector3.back * (_translationSpeed * Time.deltaTime));
        }
    }

    private void ResetMovementAfterDelay()
    {
        if (!_stopped)
            return;

        
        _stopDelayTimer += Time.deltaTime;
            if (_stopDelayTimer > _stopDelayOnDammage)
            {
                _stopped = false;
                _translationSpeed = _baseTranslationSpeed;
                _stopDelayTimer = 0f;

            }
        
    }

    private void UpdateChunks()
    {
        List<ChunkController> behindChunks = new();

        foreach (var chunk in _instancedChunks)
        {
            if (chunk.IsBehindPlayer())
            {
                behindChunks.Add(chunk);
            }
        }

        // Delete potential chunks behind player.
        if (behindChunks.Count > _behindChunkCount)
        {
            int chunkToDeleteCount = behindChunks.Count - _behindChunkCount;

            for (int i = 0; i < chunkToDeleteCount; i++)
            {
                var chunkToDelete = behindChunks[i];
                _instancedChunks.Remove(chunkToDelete);
                
                Destroy(chunkToDelete.gameObject);
            }
        }
        
        // Add potential new chunks.
        int missingChunkCount = _activeChunkCount - _instancedChunks.Count;
        for (int i = 0; i < missingChunkCount; i++)
        {
            var chunk = AddChunk(LastActiveChunk().EndAnchor);
            _instancedChunks.Add(chunk);
        }
    }


    private void AddBaseChunk()
    {
        for (int i = 0; i < _activeChunkCount; i++)
        {
            if (i == 0)
            {
                var baseChunk = AddChunk(transform.position);
                _instancedChunks.Add(baseChunk);
                continue;
            }

            var chunk = AddChunk(LastActiveChunk().EndAnchor);
            _instancedChunks.Add(chunk);
        }
    }

    private ChunkController AddChunk(Vector3 position)
    {
        if (_chunksPool.Length == 0) 
        {
            Debug.LogError("No chunks in pool");
            return null;
        }

        if (!_inShipAttackState)
        {
            var indexShip = Random.Range(0, 3);
            ChunkController chunkShip = Instantiate(_chunksPool[indexShip], position, Quaternion.identity);
        
            return chunkShip;
        }
        var index = Random.Range(0, _chunksPool.Length);
        ChunkController chunk = Instantiate(_chunksPool[index], position, Quaternion.identity);
        
        return chunk;
    }
    
    private ChunkController LastActiveChunk()
    {
        return _instancedChunks[_instancedChunks.Count - 1];
    }
}