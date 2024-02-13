using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region SAVES

    [SerializeField] private int levelNumber;
    private SaveSystem saveSystem;
    private GameProgress levelProgress;

    #endregion
    
    [Header("HUMANS")] [SerializeField] private GameObject humansParentObject;
    [SerializeField] private int maxHumanCatchAmount = 20;
    [SerializeField] private TextMeshProUGUI catchCountText;

    [Header("Coins")] [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private GameObject coinsParentObject;

    [Space] [SerializeField] private GameObject firstStageDoor;
    [SerializeField] private GameObject secondStageDoor;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWinScreen;
    [SerializeField] private Button nextLevelButton;

    [Space] [Header("SOUNDS")] 
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private int _currentCoins;

    private int _currentHumanCatched = 0;

    private int _currentZoneActive = 0;

    private bool _isPaused = false;

    public bool IsPaused => _isPaused;

    private Transform _camersTransform;

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
    }

    private void Start()
    {
        if (Camera.main != null) _camersTransform = Camera.main.transform;
        _isPaused = false;
        maxHumanCatchAmount = humansParentObject.transform.childCount;
        catchCountText.text = $"0/{maxHumanCatchAmount}";
        coinsCountText.text = "0";

        nextLevelButton.onClick.AddListener(LoadNextLevel);
        
        saveSystem = new SaveSystem();
        levelProgress = saveSystem.LoadGame();
        //levelProgress.Add(levelNumber, new LevelProgress {coinsOnLevel = coinsParentObject.transform.childCount });
        
        if (!levelProgress.Levels.ContainsKey(levelNumber))
        {
            levelProgress.Levels[levelNumber] = new LevelProgress 
            { 
                CoinsCollected = 0,
                CoinsOnLevel = coinsParentObject.transform.childCount,
                WasCompletedBefore = false,
                IsCompletedNow = false
                // coinsCollected = 0, 
                // coinsOnLevel = coinsParentObject.transform.childCount,
                // wasCompletedBefore = false, 
                // isCompletedNow = false 
            };
        }
        else
        {
            levelProgress.Levels[levelNumber].CoinsCollected = 0;
        }
    }

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

    public void ActivateZone()
    {
        _currentZoneActive++;
        if (_currentZoneActive >= 3)
        {
            // open door 2
            secondStageDoor.SetActive(false);
        }
    }

    public void DeactivateZone()
    {
        _currentZoneActive--;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        _isPaused = true;
        // при проигрыше сбрасываем количество собранных монет
        levelProgress.Levels[levelNumber].CoinsCollected = 0;
        saveSystem.SaveGame(levelProgress);
        AudioSource.PlayClipAtPoint(loseSound, _camersTransform.position);
    }

    public void GameWin()
    {
        gameWinScreen.SetActive(true);
        _isPaused = true;
        CompleteLevel(levelNumber);
        AudioSource.PlayClipAtPoint(winSound, _camersTransform.position);
    }
    
    public void CompleteLevel(int levelNumber)
    {
        if (levelProgress.Levels.ContainsKey(levelNumber))
        {
            levelProgress.Levels[levelNumber].WasCompletedBefore = true;
            levelProgress.Levels[levelNumber].IsCompletedNow = true;
        }
        else
        {
            levelProgress.Levels[levelNumber] = new LevelProgress { IsCompletedNow = true };
        }

        saveSystem.SaveGame(levelProgress);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelNumber + 2);
    }
}
