using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


public class SaveSystem
{
    private string saveFilePath;

    public SaveSystem()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    public void SaveGame( GameProgress gameProgress)
    {
        var json = JsonConvert.SerializeObject(gameProgress);
        File.WriteAllText(saveFilePath, json);
    }

    public GameProgress LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            var json = File.ReadAllText(saveFilePath);
            return JsonConvert.DeserializeObject<GameProgress>(json);
        }
        else
        {
            return new GameProgress { Levels = new Dictionary<int, LevelProgress>(), Coins = 0 };
        }
    }

    public void DeleteSaves()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class GameProgress
{
    [JsonProperty] public Dictionary<int, LevelProgress> Levels { get; set; }
    
    [JsonProperty] public int Coins { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class LevelProgress
{
    [JsonProperty]
    public int CoinsCollected { get; set; }
    
    [JsonProperty]
    public int CoinsOnLevel { get; set; }

    [JsonProperty]
    public bool WasCompletedBefore { get; set; }

    [JsonProperty]
    public bool IsCompletedNow { get; set; }
}