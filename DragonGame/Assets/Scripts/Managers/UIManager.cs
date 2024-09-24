using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButtonPause;
    [SerializeField] private Button menuButtonGameOver;
    [SerializeField] private Button menuButtonGameWin;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color countingColor;

    private bool _timerStarted = false;
    private float _timer = 0;
    
    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        continueButton.onClick.AddListener(Unpause);
        menuButtonPause.onClick.AddListener(LoadMenu);
        menuButtonGameOver.onClick.AddListener(LoadMenu);
        menuButtonGameWin.onClick.AddListener(LoadMenu);

        if (timerText)
        {
            timerText.text = "0:00";
        }
    }

    private void Update()
    {
        if (_timerStarted == false) return;

        _timer -= Time.deltaTime;
        timerText.text = String.Format("{0:0}:{1:00}", (int)_timer / 60, (int)_timer % 60);
        if (_timer <= 0)
        {
            _timerStarted = false;
            timerText.text = "0:00";
            timerText.color = normalColor;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        GameManager.Instance.TogglePause();
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        GameManager.Instance.TogglePause();
    }

    private void LoadMenu()
    {
        Unpause();
        if (CoinsMagnetItem.IsActive)
        {
            CoinsMagnetItem.IsActive = false;
        }
        SceneManager.LoadScene("Menu");
    }

    public void StartTimer(float timeInSeconds)
    {
        _timer = timeInSeconds;
        _timerStarted = true;
        timerText.color = countingColor;
    }
}
