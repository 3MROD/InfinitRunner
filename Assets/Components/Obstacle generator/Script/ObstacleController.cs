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
    [SerializeField, Tooltip("Ship Attack speed increase")] private float _speedShipAttack = 5f;
    [SerializeField, Tooltip("Ship attack SpeedUp interval")]
    private float _speedWaitShipAttack = 1f;
    
    private readonly List<ChunkController> _instancedChunks = new();
    private float _baseTranslationSpeed;
    
    private float _stopDelayTimer;
    private bool _stopped;
    private bool _inGameState;
    private bool _inShipAttackState;
    
    private GameState _gameState;
    private int _lastSpeedUpTime; // last time when SpeedUp was applied
    private int _lastBossSpeed; 

    private ShipAttackState _shipAttackState;
    private void Awake()
    {
        // listening to OnStateChange and On ShipChange
        EventSystem.OnStateChanged += HandleStateChanged;
        EventSystem.OnShipStateChange += HandleShipStateChanged; 
    }

  
    private void HandleShipStateChanged(ShipState newShipState)
    {
        // When ship State Called if in ShipAttackState set _inShipAttackState to true
        if (newShipState is not ShipAttackState shipAttackState)
        {
            _inShipAttackState = false;
            return;
        }

        _shipAttackState = shipAttackState;
        _inShipAttackState = true;
        
    }

    private void HandleStateChanged(State newState)
    {
        // When State Called if in gameState set _inGameState to true, set translation speed to base, activate Onplayer life update

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
        // start with new base chunk, no translation speed , and not in ship attack state
        _baseTranslationSpeed = _translationSpeed;
        _translationSpeed = 0;
        _inShipAttackState = false;
        AddBaseChunk();
    }

    private void OnDestroy()
    {
       EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdate;
       EventSystem.OnStateChanged -= HandleStateChanged;
    }

    private void HandlePlayerLifeUpdate(int playerLifeCount)
    {
        //if the player life is over zero set stopped to true for delay if 0 stop translation speed
        if (playerLifeCount > 0)
        {
            _stopped = true;
           // return;
        }

        _translationSpeed = 0;
    }

    private void Update()
    {
        // if in gamestate Translate + add chunks, and Reset Movement after delay )
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
        
        //in ShipAttackState Time Speeds up 
        if (_inShipAttackState && gameTimer % _speedWaitShipAttack == 0 && gameTimer != _lastBossSpeed)
            // the _lastBossSpeedup time is equal to the wait time = speed up
        {
            // the Speed of the ShipAttackState is added to the Translation speed
         _translationSpeed += _speedShipAttack * Time.deltaTime ;
         _lastBossSpeed = gameTimer; //to calculate the time since last boss speeded up
         Debug.Log("BossSpeedup");
        }
        //When ShipAttackState is over back to base translation speed
        if (!_inShipAttackState)
        {
            _translationSpeed = _baseTranslationSpeed ;
            // small time increase will playing that updates the translation speed
                 if (gameTimer != 0 && gameTimer % _speedUpInterval == 0 && gameTimer != _lastSpeedUpTime)
            {
               Debug.Log("Speed Up!");
             _translationSpeed += _speedUpIncrease * Time.deltaTime;
            _baseTranslationSpeed = _translationSpeed;
            _lastSpeedUpTime = gameTimer;
            }
        }
        
            //Translate delay  when life change 
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

        // if stopped the delay time will be added to the time; once that time is equal to delay on damage  the translation speed is back to normal all reset
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
        // new list for the chunks that are behind : behindChunks
        List<ChunkController> behindChunks = new();
        // for each chunk in the list intanceatedChunks
        foreach (var chunk in _instancedChunks)
        {
            // if the chunck controller returns : IsBehindPlayer
            if (chunk.IsBehindPlayer())
            {
                //add the chunk to the BehindChunks List
                behindChunks.Add(chunk);
            }
        }

        // Delete potential chunks behind player.
        if (behindChunks.Count > _behindChunkCount)
        {
            //if The list of behind chunks is bigger than our behindChunkCount var
            //  the list number minus the _behindCount number is the amount of chunk to be deleted
            int chunkToDeleteCount = behindChunks.Count - _behindChunkCount; 
            //as long as i is smaller thant chunkTobeDeleted the loop will continue
            for (int i = 0; i < chunkToDeleteCount; i++) 
            {
                // code that must be repeated
                var chunkToDelete = behindChunks[i];
                _instancedChunks.Remove(chunkToDelete);
                // remove from lists
                
                Destroy(chunkToDelete.gameObject);
            }
        }
        
        // Add potential new chunks.
        int missingChunkCount = _activeChunkCount - _instancedChunks.Count;
        
        for (int i = 0; i < missingChunkCount; i++)
        {
            
            var chunk = AddChunk(LastActiveChunk().EndAnchor);
            _instancedChunks.Add(chunk);
            // instantace a new chunk and add to list
        }
    }


    private void AddBaseChunk()
    {
        //if no active chunk count add base chunk and add to list then add chunks
        
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
        //Instantiate chunk Gameobjects from chunkpool list 
        if (_chunksPool.Length == 0) 
        {
            Debug.LogError("No chunks in pool");
            return null;
        }

        //if in ShipAttackState change the range of chunks that can be pooled 
        if (!_inShipAttackState)
        {
            var indexShip = Random.Range(20,_chunksPool.Length );
            ChunkController chunkShip = Instantiate(_chunksPool[indexShip], position, Quaternion.identity);
        
            return chunkShip;
        }
        var index = Random.Range(0, _chunksPool.Length);
        ChunkController chunk = Instantiate(_chunksPool[index], position, Quaternion.identity);
        
        return chunk;
    }
    
    private ChunkController LastActiveChunk()
    {
        // to get the last active chunk in the chunk list
        return _instancedChunks[_instancedChunks.Count - 1];
    }
}

