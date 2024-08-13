using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] private GameObject wallChunkPrefab;
    [SerializeField] private float chunkLength = 50f;
    [SerializeField] private int initialChunksCount = 3;
    [SerializeField] private int maxChunksAhead = 2;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> activeChunks = new List<GameObject>();
    private Queue<GameObject>[] chunkPools;
    private GameObject wallChunk;

    private void Start()
    {
        InitializeObjectPools();
        SpawnInitialChunks();
        ChunkTrigger.OnChunkTriggered += OnChunkTriggered;
        
        Invoke(nameof(UpdateNavMesh), .025f);
    }

    private void InitializeObjectPools()
    {
        chunkPools = new Queue<GameObject>[chunkPrefabs.Length];
        
        for (int i = 0; i < chunkPrefabs.Length; i++)
        {
            chunkPools[i] = new Queue<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                GameObject chunk = Instantiate(chunkPrefabs[i]);
                chunk.SetActive(false);
                chunkPools[i].Enqueue(chunk);
            }
        }
    }

    private void SpawnInitialChunks()
    {
        for (int i = 0; i < initialChunksCount; i++)
        {
            SpawnNewChunk();
        }
        SpawnWallChunk();
    }

    private void SpawnWallChunk()
    {
        if (wallChunk != null)
        {
            ReturnChunkToPool(wallChunk);
        }

        wallChunk = Instantiate(wallChunkPrefab);
        Vector3 spawnPosition = activeChunks[0].transform.position - Vector3.forward * chunkLength;
        wallChunk.transform.position = spawnPosition;
    }

    private void SpawnNewChunk()
    {
        int chunkIndex = Random.Range(0, chunkPrefabs.Length);
        GameObject newChunk = GetChunkFromPool(chunkIndex);
        
        Vector3 spawnPosition = (activeChunks.Count > 0) 
            ? activeChunks[activeChunks.Count - 1].transform.position + Vector3.forward * chunkLength
            : Vector3.zero;
        
        newChunk.transform.position = spawnPosition;
        newChunk.SetActive(true);
        
        activeChunks.Add(newChunk);
    }

    private GameObject GetChunkFromPool(int chunkIndex)
    {
        if (chunkPools[chunkIndex].Count > 0)
        {
            return chunkPools[chunkIndex].Dequeue();
        }
        else
        {
            // Если пул пуст, создаем новый объект
            return Instantiate(chunkPrefabs[chunkIndex]);
        }
    }

    private void OnChunkTriggered(ChunkTrigger trigger)
    {
        int triggeredChunkIndex = activeChunks.FindIndex(chunk => chunk.GetComponentInChildren<ChunkTrigger>() == trigger);

        // Спавним новый чанк только если нужно
        if (activeChunks.Count - triggeredChunkIndex <= maxChunksAhead)
        {
            SpawnNewChunk();
        }

        // Удаляем старые чанки и перемещаем стену
        while (triggeredChunkIndex > 0)
        {
            GameObject chunkToRemove = activeChunks[0];
            activeChunks.Remove(chunkToRemove);

            if (chunkToRemove != wallChunk)
            {
                ReturnChunkToPool(chunkToRemove);
            }
            
            triggeredChunkIndex--;
        }

        // Перемещаем стену на место первого активного чанка
        Vector3 newWallPosition = activeChunks[0].transform.position - Vector3.forward * chunkLength;
        wallChunk.transform.position = newWallPosition;
        activeChunks.Insert(0, wallChunk);
        
        UpdateNavMesh();
    }

    private void ReturnChunkToPool(GameObject chunk)
    {
        chunk.SetActive(false);
        int chunkIndex = System.Array.FindIndex(chunkPrefabs, prefab => prefab.name == chunk.name.Replace("(Clone)", ""));
        if (chunkIndex != -1) // Проверка, чтобы не добавлять wallChunk в пул обычных чанков
        {
            chunkPools[chunkIndex].Enqueue(chunk);
        }
    }

    private void UpdateNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
    
    private void OnDisable()
    {
        ChunkTrigger.OnChunkTriggered -= OnChunkTriggered;
    }
}