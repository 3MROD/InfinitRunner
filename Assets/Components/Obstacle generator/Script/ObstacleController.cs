using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleController : MonoBehaviour
{
    [Header("Parametres")]
    [SerializeField, Tooltip("translation speed of chunks in m/s")] private float _translationSpeed = 1f;

    [SerializeField] private int _activeChunkCount = 5;
    [Header("components")]
    [SerializeField] private ChunkController[] _chunckPool;
    private List<ChunkController> _instancedChunks = new();
    private void Start()
    {
        AddBaseChunk(); 
    }

    private void AddBaseChunk()
    {
        for (int i = 0; i < _activeChunkCount; i++)
        {
            if (i == 0)
            {
                var basechunk = Addchunck(transform.position);
                _instancedChunks.Add(basechunk);
                continue;
            }
            var chunk = Addchunck(LastChunk().EndAnchor);
            _instancedChunks.Add(chunk);
        }
    }

    private ChunkController Addchunck(Vector3 position)
    {
        if (_chunckPool.Length == 0)
        {
            Debug.LogError("no chunk in pool");
            return null; 
        }
        var index = Random.Range(0, _chunckPool.Length);
        ChunkController chunk = Instantiate(_chunckPool[index], position, Quaternion.identity);
        return chunk;
    }

    private ChunkController LastChunk()
    {
        return _instancedChunks[_instancedChunks.Count - 1];
    }
}
