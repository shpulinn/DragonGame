using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class SaveSystem
{
    private const string SaveFileName = "savefile.json";
    private const float SaveInterval = 30f; // Интервал автосохранения в секундах
    
    private static SaveSystem _instance;
    public static SaveSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveSystem();
            }
            return _instance;
        }
    }

    private string saveFilePath;
    private GameProgress cachedProgress;
    private int coinsSinceLastSave = 0;
    private float lastSaveTime;

    public SaveSystem()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, SaveFileName);
        cachedProgress = LoadGame();
        lastSaveTime = Time.time;
    }

    public void Update()
    {
        if (Time.time - lastSaveTime >= SaveInterval)
        {
            SaveGame();
            lastSaveTime = Time.time;
        }
    }

    public bool TrySpentDiamonds(int amount)
    {
        if (cachedProgress.Diamonds - amount >= 0)
        {
            cachedProgress.Diamonds -= amount;
            SaveGame();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddCoins(int amount)
    {
        cachedProgress.Coins += amount;
        coinsSinceLastSave += amount;

        SaveGame();
    }

    public void CompleteLevel(int levelId, int coinsCollected, int totalCoins)
    {
        if (!cachedProgress.Levels.ContainsKey(levelId))
        {
            cachedProgress.Levels[levelId] = new LevelProgress();
        }

        var level = cachedProgress.Levels[levelId];
        level.CoinsCollected = coinsCollected;
        level.CoinsOnLevel = totalCoins;
        level.IsCompletedNow = true;
        level.WasCompletedBefore = true;

        SaveGame(); // Всегда сохраняем при завершении уровня
    }

    public async void SaveGame()
    {
        try
        {
            var json = JsonConvert.SerializeObject(cachedProgress, Formatting.Indented);
            await File.WriteAllTextAsync(saveFilePath, json);
            Debug.Log($"Game saved successfully to {saveFilePath}");
            coinsSinceLastSave = 0;
            lastSaveTime = Time.time; // Обновляем время последнего сохранения
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save game: {ex.Message}");
        }
    }

    public GameProgress LoadGame()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                var json = File.ReadAllText(saveFilePath);
                cachedProgress = JsonConvert.DeserializeObject<GameProgress>(json);
                Debug.Log("Game loaded successfully");
            }
            else
            {
                Debug.Log("No save file found. Creating new game progress.");
                cachedProgress = new GameProgress();
                cachedProgress.Diamonds += 30;
                cachedProgress.Coins += 10;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load game: {ex.Message}");
            cachedProgress = new GameProgress();
        }

        return cachedProgress;
    }

    public GameProgress GetCurrentProgress()
    {
        return cachedProgress;
    }

    public async Task<bool> DeleteSaves()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                await Task.Run(() => File.Delete(saveFilePath));
                cachedProgress = new GameProgress();
                coinsSinceLastSave = 0;
                Debug.Log("Save file deleted successfully");
                return true;
            }
            else
            {
                Debug.Log("No save file found to delete");
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to delete save file: {ex.Message}");
            return false;
        }
    }
}

[Serializable]
public class GameProgress
{
    public Dictionary<int, LevelProgress> Levels { get; set; } = new Dictionary<int, LevelProgress>();
    public int Coins { get; set; }
    public int Diamonds { get; set; }
}

[Serializable]
public class LevelProgress
{
    public int CoinsCollected { get; set; }
    public int CoinsOnLevel { get; set; }
    public bool WasCompletedBefore { get; set; }
    public bool IsCompletedNow { get; set; }
}