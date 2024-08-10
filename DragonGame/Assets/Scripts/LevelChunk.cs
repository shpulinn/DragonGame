using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private List<GameObject> humans;

    private void OnEnable()
    {
        if (coins.Count > 0)
            EnableEntities(coins);
        if (humans.Count > 0)
            EnableEntities(humans);
    }

    private void EnableEntities(List<GameObject> objects)
    {
        foreach (GameObject o in objects)
        {
            o.SetActive(true);
        }
    }
}
