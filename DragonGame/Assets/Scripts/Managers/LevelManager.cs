using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isBuffOnLevel;
    [SerializeField] private StageDoor stageDoor;
    
    public void CompleteFirstStage()
    {
        if (isBuffOnLevel)
        {
            LevelBuff levelBuff = FindObjectOfType<LevelBuff>();
            if (levelBuff != null)
            {
                levelBuff.ApplyBuff();
            }
        }
        else
        {
            // другая логика выполнения 1 стадии (например открытие двери)
            //stageDoor.SetActive(false);
            stageDoor.OpenDoor();
        }
    }
}
