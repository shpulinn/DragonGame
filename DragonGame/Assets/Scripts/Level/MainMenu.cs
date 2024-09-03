using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject levelsScreen;
    [SerializeField] private GameObject shopScreen;

    [Header("Buttons")] 
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitSettingsButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button exitLevelsButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    
    private SaveSystem _saveSystem;
    private GameProgress _levelProgress;
    private int maxLevelNumber = 1;

    private void Start()
    {
        settingsButton.onClick.AddListener(OpenSettingsButton);
        exitSettingsButton.onClick.AddListener(CloseSettingsButton);
        levelsButton.onClick.AddListener(OpenLevelsButton);
        exitLevelsButton.onClick.AddListener(CloseLevelsButton);
        playButton.onClick.AddListener(PlayButton);
        shopButton.onClick.AddListener(ShopButton);
        
        _saveSystem = new SaveSystem();
        _levelProgress = _saveSystem.LoadGame();

        if (_levelProgress.Levels.Count < 1)
        {
            return;
        }
        if (_levelProgress.Levels[_levelProgress.Levels.Count - 1].WasCompletedBefore)
        {
            maxLevelNumber = _levelProgress.Levels.Count + 1;
        }
        else maxLevelNumber = _levelProgress.Levels.Count;
    }

    private void OpenSettingsButton() => settingsScreen.SetActive(true);
    private void CloseSettingsButton() => settingsScreen.SetActive(false);
    private void OpenLevelsButton() => levelsScreen.SetActive(true);
    private void CloseLevelsButton() => levelsScreen.SetActive(false);

    private void ShopButton() => shopScreen.SetActive(true);

    private void PlayButton()
    {
        // for now, infinite level is last of the list
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
