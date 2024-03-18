using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuff : MonoBehaviour
{
    [SerializeField] private Vector3 newScaleAmount;
    [SerializeField] private float newSpaceBetweenPartsAmount;
    [SerializeField] private float buffDuration;

    private Dragon _dragon;

    private void Start()
    {
        _dragon = FindObjectOfType<Dragon>();
    }

    public void ApplyBuff()
    {
        _dragon.ApplyScaleBuff(newScaleAmount, newSpaceBetweenPartsAmount, buffDuration);

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
        _dragon.RemoveBuff();
    }
}
