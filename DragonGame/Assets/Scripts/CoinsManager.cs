using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;

    public Event OnCoinAdd;
    
    private int _currentCoins;
    
    private SaveSystem _saveSystem;
    private GameProgress _levelProgress;

    public int CurrentCoins => _currentCoins;

    private void OnEnable()
    {
        _saveSystem = new SaveSystem();
        _levelProgress = _saveSystem.LoadGame();

        _currentCoins = _levelProgress.Coins;
        
        GlobalEventManager.OnCoinCollected.AddListener(AddCoins);
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
        _currentCoins += amount;
        _levelProgress.Coins = _currentCoins;
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