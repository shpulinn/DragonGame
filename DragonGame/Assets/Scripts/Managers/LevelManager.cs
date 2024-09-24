using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isBuffOnLevel;
    [SerializeField] private DragonBuffs dragonBuffs;
    [SerializeField] private StageDoor stageDoor;
    
    public void CompleteFirstStage()
    {
        if (isBuffOnLevel && dragonBuffs)
        {
            
            LevelBuff levelBuff = FindObjectOfType<LevelBuff>();
            if (levelBuff != null)
            {
                levelBuff.ApplyBuff();
            }
            if (BuffManager.Instance.IsAnyBuffActive)
            {
                return;
            }
            BuffManager.Instance.ActivateBuff(dragonBuffs);
        }
        else
        {
            // другая логика выполнения 1 стадии (например открытие двери)
            //stageDoor.SetActive(false);
            stageDoor.OpenDoor();
        }
    }
}
