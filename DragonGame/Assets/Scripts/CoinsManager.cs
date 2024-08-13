﻿using ScriptableObjects;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;

    public Event OnCoinAdd;
    
    private int _currentCoins;
    
    private SaveSystem _saveSystem;
    private GameProgress _levelProgress;

    [SerializeField] private DragonBuffs dragonBuff;

    public int CurrentCoins => _currentCoins;

    private void OnEnable()
    {
        _saveSystem = new SaveSystem();
        _levelProgress = _saveSystem.LoadGame();

        _currentCoins = _levelProgress.Coins;
        
        GlobalEventManager.OnCoinCollected.AddListener(AddCoins);
        GlobalEventManager.OnProgressReached.AddListener(ActivateCoinsMultiplier);
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than 1 instance of CoinsManager");
            return;
        }
        Instance = this;
    }

    public void AddCoins(int amount = 1)
    {
        if (amount < 1)
        {
            return;
        }

        if (BuffManager.Instance.IsAnyBuffActive)
        {
            if (BuffManager.Instance.IsBuffActive(dragonBuff.buffName))
            {
                amount *= BuffManager.Instance.GetCoinMultiplier();
            }
        }
        _currentCoins += amount;
        _levelProgress.Coins = _currentCoins;
    }

    public void ActivateCoinsMultiplier(int amount)
    {
        if (amount > 0)
            return;
        // cant activate new buff, while some buff is active
        if (BuffManager.Instance.IsAnyBuffActive)
            return;
        BuffManager.Instance.ActivateBuff(dragonBuff);
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount < 1)
        {
            Debug.Log("cant spend " + amount + " money");
            return false;
        }
        if (_currentCoins - amount >= 0)
        {
            return false;
        }

        _currentCoins -= amount;
        _levelProgress.Coins = _currentCoins;
        return true;
    }
}