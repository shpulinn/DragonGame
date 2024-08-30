using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

public class LevelChunk : MonoBehaviour
{
    [SerializeField] private ChunkSettings chunkSettings;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform diamondSpawnPoint;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject diamondPrefab;
    [SerializeField] private GameObject humanPrefab;
    [SerializeField] private List<GameObject> trapPrefabs;

    [SerializeField] private List<GameObject> coins;
    [SerializeField] private List<GameObject> humans;
    [SerializeField] private List<GameObject> traps;
    
    private IObjectPool<GameObject> coinsPool;
    private IObjectPool<GameObject> humansPool;
    private IObjectPool<GameObject> trapsPool;
    
    private void Awake()
    {
        coinsPool = new ObjectPool<GameObject>(() => Instantiate(coinPrefab), OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, true, 10, 20);
        humansPool = new ObjectPool<GameObject>(() => Instantiate(humanPrefab), OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, true, 5, 10);
        trapsPool = new ObjectPool<GameObject>(() => Instantiate(trapPrefabs[0]), OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, true, 5, 10);
    }

    private void OnTakeFromPool(GameObject obj) => obj.SetActive(true);
    private void OnReturnToPool(GameObject obj) => obj.SetActive(false);
    private void OnDestroyPoolObject(GameObject obj) => Destroy(obj);
    
    private void OnEnable()
    {
        coins = new List<GameObject>();
        humans = new List<GameObject>();
        traps = new List<GameObject>();
        /*
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            float randomValue = UnityEngine.Random.value;
            float totalChance = chunkSettings.coinChance + chunkSettings.humanChance + chunkSettings.trapChance + chunkSettings.emptyChance;

            if (randomValue < chunkSettings.emptyChance / totalChance)
            {
                // nothing to spawn
                continue;
            }
            else if (randomValue < (chunkSettings.emptyChance + chunkSettings.coinChance) / totalChance)
            {
                SpawnObject(coinPrefab, spawnPoints[i], coins);
                continue;
            }
            else if (randomValue < (chunkSettings.emptyChance + chunkSettings.coinChance + chunkSettings.humanChance) / totalChance)
            {
                SpawnObject(humanPrefab, spawnPoints[i], humans);
                continue;
            }
            else
            {
                GameObject randomTrapPrefab = trapPrefabs[UnityEngine.Random.Range(0, trapPrefabs.Count)];
                SpawnObject(randomTrapPrefab, spawnPoints[i], traps);
            }
        }
        */
        
        float totalChance = chunkSettings.coinChance + chunkSettings.humanChance + chunkSettings.trapChance + chunkSettings.emptyChance;
        float coinThreshold = chunkSettings.emptyChance + chunkSettings.coinChance;
        float humanThreshold = coinThreshold + chunkSettings.humanChance;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            float randomValue = UnityEngine.Random.value * totalChance;

            if (randomValue < chunkSettings.emptyChance)
            {
                continue;
            }
            else if (randomValue < coinThreshold)
            {
                SpawnObject(coinPrefab, spawnPoints[i], coins);
            }
            else if (randomValue < humanThreshold)
            {
                SpawnObject(humanPrefab, spawnPoints[i], humans);
            }
            else
            {
                GameObject randomTrapPrefab = trapPrefabs[UnityEngine.Random.Range(0, trapPrefabs.Count)];
                SpawnObject(randomTrapPrefab, spawnPoints[i], traps);
            }
        }


        if (CoinsManager.Instance.IsDiamondsActive && diamondPrefab)
        {
            SpawnObject(diamondPrefab, diamondSpawnPoint);
        }
    }
    
    private void SpawnObject(GameObject prefab, Transform spawnPoint, List<GameObject> list = null)
    {
        /*
        GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform);
        if (list != null)
        {
            list.Add(spawnedObject);
        }
        */
        
        GameObject spawnedObject;
        if (prefab == coinPrefab)
            spawnedObject = coinsPool.Get();
        else if (prefab == humanPrefab)
            spawnedObject = humansPool.Get();
        else
            spawnedObject = trapsPool.Get();

        spawnedObject.transform.position = spawnPoint.position;
        spawnedObject.transform.rotation = Quaternion.identity;
        spawnedObject.transform.SetParent(transform);

        if (list != null)
        {
            list.Add(spawnedObject);
        }
    }

    private void EnableEntities(List<GameObject> objects)
    {
        foreach (GameObject o in objects)
        {
            o.SetActive(true);
        }
    }

    private void OnDisable()
    {
        /*
        // clear chunk
        foreach (var o in coins)
        {
            Destroy(o);
        }
        foreach (var o in humans)
        {
            Destroy(o);
        }
        foreach (var o in traps)
        {
            Destroy(o);
        }
        coins.Clear();
        humans.Clear();
        traps.Clear();
        */
        ReturnObjectsToPool(coins, coinsPool);
        ReturnObjectsToPool(humans, humansPool);
        ReturnObjectsToPool(traps, trapsPool);
    
        coins.Clear();
        humans.Clear();
        traps.Clear();
    }
    
    private void ReturnObjectsToPool(List<GameObject> objects, IObjectPool<GameObject> pool)
    {
        foreach (var obj in objects)
        {
            pool.Release(obj);
        }
    }
}
