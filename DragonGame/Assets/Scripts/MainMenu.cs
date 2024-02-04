using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject levelsScreen;

    [Header("Buttons")] 
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitSettingsButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button exitLevelsButton;
    [SerializeField] private Button playButton;

    private void Start()
    {
        settingsButton.onClick.AddListener(OpenSettingsButton);
        exitSettingsButton.onClick.AddListener(CloseSettingsButton);
        levelsButton.onClick.AddListener(OpenLevelsButton);
        exitLevelsButton.onClick.AddListener(CloseLevelsButton);
        playButton.onClick.AddListener(PlayButton);
    }

    private void OpenSettingsButton() => settingsScreen.SetActive(true);
    private void CloseSettingsButton() => settingsScreen.SetActive(false);
    private void OpenLevelsButton() => levelsScreen.SetActive(true);
    private void CloseLevelsButton() => levelsScreen.SetActive(false);

    private void PlayButton()
    {
        // logic
        SceneManager.LoadScene(1);
    }
}
