using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    [SerializeField] private GameObject UIprefab;
    [SerializeField] private Sprite standartIcon;

    private Dictionary<string, Achievement> achievements;

    private GameObject _canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }

        //_canvas = GameObject.Find("MainCanvas");
    }

    private void Initialize()
    {
        achievements = new Dictionary<string, Achievement>();
        // Здесь можно добавить достижения

        Achievement firstBuffEver = new Achievement();
        firstBuffEver.Id = "0";
        firstBuffEver.Description = "You just got a buff!";
        firstBuffEver.icon = standartIcon;
        
        RegisterAchievement(firstBuffEver);
    }

    public void RegisterAchievement(Achievement achievement)
    {
        if (!achievements.ContainsKey(achievement.Id))
        {
            achievements.Add(achievement.Id, achievement);
        }
    }

    public void UnlockAchievement(string id)
    {
        if (achievements.TryGetValue(id, out var achievement))
        {
            achievement.Unlock();
            _canvas = GameObject.Find("MainCanvas");
            GameObject UIprefab = Instantiate(this.UIprefab, _canvas.transform);
            UIprefab.GetComponent<AchievementPopup>().ShowAchievement(achievement.Description, achievement.icon);
        }
    }

    public bool IsAchievementUnlocked(string id)
    {
        if (achievements.TryGetValue(id, out var achievement))
        {
            return achievement.IsUnlocked;
        }
        return false;
    }
}