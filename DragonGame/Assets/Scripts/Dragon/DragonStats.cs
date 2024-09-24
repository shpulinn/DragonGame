using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DragonStats")]
public class DragonStats : ScriptableObject
{
    #region Constants

    private const string MAX_LIVES_INT = "MaxLives";
    private const string LIVES_CURRENT_UPGRADE_AMOUNT_INT = "LivesCurrentUpgradeAmount";
    private const string LIVES_UPGRADES_TO_LEVELUP_INT = "LivesUpgradesToLevelUp";

    private const string DESTRUCTION_LEVEL_INT = "DestructionLevel";
    private const string DESTRUCTION_CURRENT_UPGRADE_AMOUNT_INT = "DestructionCurrentUpgradeAmount";
    private const string DESTRUCTION_UPGRADES_TO_LEVELUP_INT = "DestructionUpgradesToLevelUp";
    
    private const string DAMAGE_INT = "AttackDamageLevel";
    private const string DAMAGE_CURRENT_UPGRADE_AMOUNT_INT = "AttackDamageCurrentUpgradeAmount";
    private const string DAMAGE_UPGRADES_TO_LEVELUP_INT = "AttackDamageUpgradesToLevelUp";
    
    #endregion
    
    
    [Header("Lives")]
    public int MaxLives = 1;
    public int LivesUpgradeCost = 1;
    [Tooltip("Current level improvement progress")]
    public int LivesCurrentUpgradeAmount = 0;
    [Tooltip("How many times you need to buy to upgrade a level")]
    public int LivesUpgradesToLevelUp = 10;

    [Header("Destruction")] public int DestructionLevel = 1;
    public int DestructionUpgradeCost = 1;
    [Tooltip("Current level improvement progress")]
    public int DestructionCurrentUpgradeAmount = 0;
    [Tooltip("How many times you need to buy to upgrade a level")]
    public int DestructionUpgradesToLevelUp = 10;
    
    [Header("Attack Damage")]
    public int AttackDamageLevel = 1;
    public int AttackDamageUpgradeCost = 1;
    [Tooltip("Current level improvement progress")]
    public int AttackDamageCurrentUpgradeAmount = 0;
    [Tooltip("How many times you need to buy to upgrade a level")]
    public int AttackDamageUpgradesToLevelUp = 10;

    public void UpgradeLive()
    {
        LivesCurrentUpgradeAmount++;
        if (LivesCurrentUpgradeAmount == LivesUpgradesToLevelUp)
        {
            LivesCurrentUpgradeAmount = 0;
            MaxLives++;
        }
        SaveData();
    }
    
    public void UpgradeDestruction()
    {
        DestructionCurrentUpgradeAmount++;
        if (DestructionCurrentUpgradeAmount == DestructionUpgradesToLevelUp)
        {
            DestructionCurrentUpgradeAmount = 0;
            DestructionLevel++;
        }
        SaveData();
    }
    
    public void UpgradeAttackDamage()
    {
        AttackDamageCurrentUpgradeAmount++;
        if (AttackDamageCurrentUpgradeAmount == AttackDamageUpgradesToLevelUp)
        {
            AttackDamageCurrentUpgradeAmount = 0;
            AttackDamageLevel++;
        }
        SaveData();
    }

    private void SaveData()
    {
        // LIVES
        PlayerPrefs.SetInt(MAX_LIVES_INT, MaxLives);
        PlayerPrefs.SetInt(LIVES_UPGRADES_TO_LEVELUP_INT, LivesUpgradesToLevelUp);
        PlayerPrefs.SetInt(LIVES_CURRENT_UPGRADE_AMOUNT_INT, LivesCurrentUpgradeAmount);
        
        // DESTRUCTION
        PlayerPrefs.SetInt(DESTRUCTION_LEVEL_INT, DestructionLevel);
        PlayerPrefs.SetInt(DESTRUCTION_UPGRADES_TO_LEVELUP_INT, DestructionUpgradesToLevelUp);
        PlayerPrefs.SetInt(DESTRUCTION_CURRENT_UPGRADE_AMOUNT_INT, DestructionCurrentUpgradeAmount);
        
        // ATTACK DAMAGE
        PlayerPrefs.SetInt(DAMAGE_INT, AttackDamageLevel);
        PlayerPrefs.SetInt(DAMAGE_UPGRADES_TO_LEVELUP_INT, AttackDamageUpgradesToLevelUp);
        PlayerPrefs.SetInt(DAMAGE_CURRENT_UPGRADE_AMOUNT_INT, AttackDamageCurrentUpgradeAmount);
    }

    public void LoadData()
    {
        // LIVES
        MaxLives = PlayerPrefs.GetInt(MAX_LIVES_INT, 1);
        LivesCurrentUpgradeAmount = PlayerPrefs.GetInt(LIVES_CURRENT_UPGRADE_AMOUNT_INT, 0);
        LivesUpgradesToLevelUp = PlayerPrefs.GetInt(LIVES_UPGRADES_TO_LEVELUP_INT, 10);
        
        // DESTRUCTION
        DestructionLevel = PlayerPrefs.GetInt(DESTRUCTION_LEVEL_INT, 1);
        DestructionCurrentUpgradeAmount = PlayerPrefs.GetInt(DESTRUCTION_CURRENT_UPGRADE_AMOUNT_INT, 0);
        DestructionUpgradesToLevelUp = PlayerPrefs.GetInt(DESTRUCTION_UPGRADES_TO_LEVELUP_INT, 10);
        
        // ATTACK DAMAGE
        AttackDamageLevel = PlayerPrefs.GetInt(DAMAGE_INT, 1);
        AttackDamageCurrentUpgradeAmount = PlayerPrefs.GetInt(DAMAGE_CURRENT_UPGRADE_AMOUNT_INT, 0);
        AttackDamageUpgradesToLevelUp = PlayerPrefs.GetInt(DAMAGE_UPGRADES_TO_LEVELUP_INT, 10);
    }
}