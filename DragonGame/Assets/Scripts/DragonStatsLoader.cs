using System;
using UnityEngine;

public class DragonStatsLoader : MonoBehaviour
{
    [SerializeField] private DragonStats DragonStats;

    private void Start()
    {
        DragonStats.LoadData();
    }
}