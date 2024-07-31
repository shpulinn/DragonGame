using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuff : MonoBehaviour
{
    [SerializeField] private Vector3 newScaleAmount;
    [SerializeField] private float newSpaceBetweenPartsAmount;
    [SerializeField] private float buffDuration;

    private DragonBuffs _dragonBuffs;

    private void Start()
    {
        _dragonBuffs = FindObjectOfType<DragonBuffs>();
    }

    public void ApplyBuff()
    {
        _dragonBuffs.ApplyScaleBuff(newScaleAmount, newSpaceBetweenPartsAmount, buffDuration);

        if (AchievementManager.Instance.IsAchievementUnlocked("0"))
        {
            return;
        }
        else
        {
            AchievementManager.Instance.UnlockAchievement("0");
        }
    }

    public void RemoveBuff()
    {
        _dragonBuffs.RemoveBuff();
    }
}
