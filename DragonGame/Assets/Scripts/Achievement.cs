using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string Id;
    public string Description;
    public Sprite icon;
    private bool isUnlocked;

    public bool IsUnlocked
    {
        get { return isUnlocked; }
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log("Achievement Unlocked: " + Description);
            // Здесь можно добавить дополнительные действия при открытии достижения
        }
    }
}