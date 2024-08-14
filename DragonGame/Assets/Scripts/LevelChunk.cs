using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

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

    //TODO: добавить списки для объектов, которые гарантированно будут находиться на уровне
    // например, группа монет или людей

    private void OnEnable()
    {
        coins = new List<GameObject>();
        humans = new List<GameObject>();
        traps = new List<GameObject>();
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

        if (CoinsManager.Instance.IsDiamondsActive && diamondPrefab)
        {
            SpawnObject(diamondPrefab, diamondSpawnPoint);
        }
    }
    
    private void SpawnObject(GameObject prefab, Transform spawnPoint, List<GameObject> list = null)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform);
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
    }
}
