using System.Collections;
using System.Collections.Generic;
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
    
    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        continueButton.onClick.AddListener(Unpause);
        menuButtonPause.onClick.AddListener(LoadMenu);
        menuButtonGameOver.onClick.AddListener(LoadMenu);
        menuButtonGameWin.onClick.AddListener(LoadMenu);
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
        SceneManager.LoadScene("Menu");
    }
}
