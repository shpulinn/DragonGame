using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopup : MonoBehaviour
{
    public float onScreenDuration = 3f;
    
    public GameObject popupPanel; // Ссылка на панель, содержащую информацию о достижении
    public TextMeshProUGUI descriptionText; // Ссылка на текстовый элемент для описания
    public Image iconImage; // Ссылка на элемент изображения для иконки

    public void ShowAchievement(string description, Sprite icon)
    {
        descriptionText.text = description;
        iconImage.sprite = icon;
        popupPanel.SetActive(true);
        Invoke(nameof(HideAchievement), onScreenDuration);
    }

    public void HideAchievement()
    {
        popupPanel.SetActive(false);
    }
}