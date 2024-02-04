using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private List<Image> stars;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite goldStar;

    private Button _btn;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SceneManager.LoadScene(levelNumber + 1);
    }

    public void SetStars(int coinsOnLevelMax, int coinsCollectedAmount)
    {
        float coinsPercentage = coinsCollectedAmount / (float)coinsOnLevelMax;

        if (coinsPercentage.Equals(0))
        {
            stars[0].sprite = emptyStar;
            stars[1].sprite = emptyStar;
            stars[2].sprite = emptyStar;
        }
        
        if (coinsPercentage is > 0 and <= .5f)
        {
            stars[0].sprite = goldStar;
        }

        if (coinsPercentage is >= .51f and <= .98f)
        {
            stars[0].sprite = goldStar;
            stars[1].sprite = goldStar;
        }

        if (coinsPercentage >= .99f)
        {
            stars[0].sprite = goldStar;
            stars[1].sprite = goldStar;
            stars[2].sprite = goldStar;
        }
    }
}
