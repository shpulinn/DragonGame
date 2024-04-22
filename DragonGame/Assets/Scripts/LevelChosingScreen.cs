using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChosingScreen : MonoBehaviour
{
    [SerializeField] private List<LevelButton> levelButtons;
    [SerializeField] private GameObject loadingScreen;

    private SaveSystem _saveSystem;
    private GameProgress _levelProgress;

    private void OnEnable()
    {
        foreach (var button in levelButtons)
        {
            button.GetComponent<Button>().interactable = false;
            button.SetStars(1,0);
        }
        _saveSystem = new SaveSystem();
        _levelProgress = _saveSystem.LoadGame();
        // for (int i = 0; i < _levelProgress.Levels.Count; i++)
        // {
        //     Debug.Log(_levelProgress.Levels[i].CoinsCollected);
        // }
        if (_levelProgress.Levels.Count < 1)
        {
            // при первом запуске активируем только первый уровень
            levelButtons[0].GetComponent<Button>().interactable = true;
            return;
        }
        for (int i = 0; i < _levelProgress.Levels.Count; i++)
        {
            levelButtons[i].GetComponent<Button>().interactable = true;
            levelButtons[i].SetStars(_levelProgress.Levels[i].CoinsOnLevel, _levelProgress.Levels[i].CoinsCollected);
        }

        // ставим доступным уровень, следующий за крайним пройденным на данный момент
        if (_levelProgress.Levels[_levelProgress.Levels.Count - 1].WasCompletedBefore)
        {
            Debug.LogWarning(_levelProgress.Levels.Count + " НЕ ЗАБУДЬ ВЫКЛЮЧИТЬ ОСТАНОВКУ ПРОГРЕССА");
            if (_levelProgress.Levels.Count >= 4)
            {
                return;
            }
            levelButtons[_levelProgress.Levels.Count].GetComponent<Button>().interactable = true;
        }
    }

    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
}

/* 0--------------------------------------------------------------------0
 
 Добавить в сейв поле хранящее кол-во мнет, собранных в прошлый раз. если их больше 0,
 то не убирать эту инфу и хранить в рекорде. если кол-во собранных монет в текущей попытке
 меньше чем в этом поле, то рекорд не обновлять. если больше, то обновлять рекорд и кол-во звездочек соотв.
 */
