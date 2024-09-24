using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimedShop : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private List<GameObject> shopItems; 
        private int _currentItemIndex = 0;
        private const string TIME_KEY = "ShopItemStartTime";
        private const float TimerDuration = 24 * 60 * 60; // 24 часа в секундах

        private void Start()
        {
            if (!PlayerPrefs.HasKey(TIME_KEY))
            {
                ResetTimer();
            }
            InvokeRepeating(nameof(UpdateTimer), 0f, 1f);
        }

        private void ResetTimer()
        {
            PlayerPrefs.SetString(TIME_KEY, DateTime.Now.ToBinary().ToString());
            PlayerPrefs.Save();
            UpdateItems();
        }

        private void UpdateItems()
        {
            HideAllShopItems();
            _currentItemIndex = (_currentItemIndex + 1) % shopItems.Count;
            ShowShopItemByIndex(_currentItemIndex);
        }

        private void HideAllShopItems()
        {
            for (int i = 0; i < shopItems.Count; i++)
            {
                shopItems[i].SetActive(false);
            }
        }

        private void ShowShopItemByIndex(int index)
        {
            shopItems[index].SetActive(true);
        }

        private void UpdateTimer()
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString(TIME_KEY));
            DateTime startTime = DateTime.FromBinary(temp);
            TimeSpan elapsed = DateTime.Now - startTime;

            if (elapsed.TotalSeconds >= TimerDuration)
            {
                ResetTimer();
                elapsed = TimeSpan.Zero;
            }

            TimeSpan remaining = TimeSpan.FromSeconds(TimerDuration) - elapsed;
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                remaining.Hours, remaining.Minutes, remaining.Seconds);
        }
    }
}