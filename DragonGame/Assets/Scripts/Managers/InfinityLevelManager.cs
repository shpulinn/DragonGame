using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfinityLevelManager : MonoBehaviour
{
    public delegate void OnGameOver();

    public OnGameOver OnGameOverEvent;

    public static InfinityLevelManager Instance;

    [SerializeField] private Slider progressSlider;
    [Space]
    [SerializeField] private GameObject gameOverScreen;

    [Space] [Header("SOUNDS")] 
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private int _currentCoins;

    private int _currentHumanCatched = 0;

    private bool _isPaused = false;

    public bool IsPaused => _isPaused;

    private Transform _cameraTransform;

    private LevelManager _levelManager;

    public void TogglePause()
    {
        _isPaused = !_isPaused;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than 1 instance");
            return;
        }

        Instance = this;

        _levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void Start()
    {
        if (Camera.main != null) _cameraTransform = Camera.main.transform;
        _isPaused = false;
        
        GlobalEventManager.OnCoinCollected.AddListener(AddCoins);
        GlobalEventManager.OnHumanCollected.AddListener(AddHuman);
    }

    private void AddCoins(int amount)
    {
        progressSlider.value -= amount;
        if (progressSlider.value == 0)
        {
            HandleProgress(0);
        }
    }

    private void AddHuman()
    {
        progressSlider.value += 1;
        if ((int)progressSlider.value == (int)progressSlider.maxValue)
        {
            HandleProgress((int)progressSlider.maxValue);
        }
    }

    public void HandleProgress(int amount)
    {
        GlobalEventManager.SendProgressReached(amount);
        progressSlider.value = progressSlider.maxValue / 2;
    }
    
    /*
    public void AddCoin()
    {
        _currentCoins++;
        coinsCountText.text = _currentCoins.ToString();
        
        if (levelProgress.Levels.ContainsKey(levelNumber))
        {
            levelProgress.Levels[levelNumber].CoinsCollected++;
        }
        else
        {
            levelProgress.Levels[levelNumber] = new LevelProgress { CoinsCollected = 1 };
        }
        //saveSystem.SaveGame(levelProgress);
    } 

    public void CatchHuman()
    {
        _currentHumanCatched++;
        catchCountText.text = $"{_currentHumanCatched}/{maxHumanCatchAmount}";
        if (_currentHumanCatched >= maxHumanCatchAmount)
        {
            Debug.Log("GAME OVER => HUMANS");
            GameOver();
        }
    }
    */
    public void GameOver()
    {
        if (!_isPaused)
        {
            AudioSource.PlayClipAtPoint(loseSound, _cameraTransform.position);
        }
        gameOverScreen.SetActive(true);
        OnGameOverEvent?.Invoke();
        _isPaused = true;
        // при проигрыше сбрасываем количество собранных монет
        //levelProgress.Levels[levelNumber].CoinsCollected = 0;
        //saveSystem.SaveGame(levelProgress);
    }
    
}
