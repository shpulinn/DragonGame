using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinsLabel;
    [SerializeField] private Button backButton;
    [Space]
    [SerializeField] private Button LivesUpgradeButton;
    [SerializeField] private Button DestructionUpgradeButton;
    [SerializeField] private Button AttackDamageUpgradeButton;
    [Space]
    [SerializeField] private Slider LivesProgressBar;
    [SerializeField] private Slider DestructionProgressBar;
    [SerializeField] private Slider AttackDamageProgressBar;
    [Header("Levels screen")][SerializeField] private GameObject statsLevelsScreen;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI LivesLevelText;
    [SerializeField] private TextMeshProUGUI DestructionLevelText;
    [SerializeField] private TextMeshProUGUI AttackDamageLevelText;
    [SerializeField] private Color normalTextColor;
    [SerializeField] private Color highlightedTextColor;
    [Space] [SerializeField] private GameObject NotEnoughCoinsMessage;

    [Header("SO")][SerializeField] private DragonStats DragonStats;
    
    private SaveSystem saveSystem;
    private GameProgress levelProgress;

    private void OnEnable()
    {
        saveSystem = new SaveSystem();
        levelProgress = saveSystem.LoadGame();
        
        // set up buttons
        backButton.onClick.AddListener(CloseShop);
        LivesUpgradeButton.onClick.AddListener(UpgradeLives);
        DestructionUpgradeButton.onClick.AddListener(UpgradeDestruction);
        AttackDamageUpgradeButton.onClick.AddListener(UpgradeAttackDamage);

        coinsLabel.text = saveSystem.GetCurrentProgress().Coins.ToString();
        
        DragonStats.LoadData();
        UpdateUI();
    }

    private void UpdateUI()
    {
        LivesProgressBar.maxValue = DragonStats.LivesUpgradesToLevelUp;
        LivesProgressBar.value = DragonStats.LivesCurrentUpgradeAmount;
        LivesLevelText.text = DragonStats.MaxLives.ToString();

        DestructionProgressBar.maxValue = DragonStats.DestructionUpgradesToLevelUp;
        DestructionProgressBar.value = DragonStats.DestructionCurrentUpgradeAmount;
        DestructionLevelText.text = DragonStats.DestructionLevel.ToString();

        AttackDamageProgressBar.maxValue = DragonStats.AttackDamageUpgradesToLevelUp;
        AttackDamageProgressBar.value = DragonStats.AttackDamageCurrentUpgradeAmount;
        AttackDamageLevelText.text = DragonStats.AttackDamageLevel.ToString();
        
        LivesLevelText.color = normalTextColor;
        DestructionLevelText.color = normalTextColor;
        AttackDamageLevelText.color = normalTextColor;

        coinsLabel.text = CoinsManager.Instance.CurrentCoins.ToString();
        SaveSystem.Instance.SaveGame();
    }

    private void UpgradeLives()
    {
        if (CoinsManager.Instance.TrySpendCoins(DragonStats.LivesUpgradeCost))
        {
            var levelBeforeUpgrade = DragonStats.MaxLives;
            DragonStats.UpgradeLive();
            UpdateUI();
            if (DragonStats.MaxLives > levelBeforeUpgrade)
            {
                //statsLevelsScreen.SetActive(true);
                LivesLevelText.color = highlightedTextColor;
            }
        } else 
            ShowNotEnoughCoinsMessage();
    }

    private void UpgradeDestruction()
    {
        if (CoinsManager.Instance.TrySpendCoins(DragonStats.DestructionUpgradeCost))
        {
            var levelBeforeUpgrade = DragonStats.DestructionLevel;
            DragonStats.UpgradeDestruction();
            UpdateUI();
            if (DragonStats.DestructionLevel > levelBeforeUpgrade)
            {
                //statsLevelsScreen.SetActive(true);
                DestructionLevelText.color = highlightedTextColor;
            }
        } else 
            ShowNotEnoughCoinsMessage();
    }

    private void UpgradeAttackDamage()
    {
        if (CoinsManager.Instance.TrySpendCoins(DragonStats.AttackDamageUpgradeCost))
        {
            var levelBeforeUpgrade = DragonStats.AttackDamageLevel;
            DragonStats.UpgradeAttackDamage();
            UpdateUI();
            if (DragonStats.AttackDamageLevel > levelBeforeUpgrade)
            {
                //statsLevelsScreen.SetActive(true);
                AttackDamageLevelText.color = highlightedTextColor;
            }
        } else 
            ShowNotEnoughCoinsMessage();
    }

    private void ShowNotEnoughCoinsMessage()
    {
        NotEnoughCoinsMessage.SetActive(true);
        Invoke(nameof(HideNotEnoughCoinsMessage), 1.5f);
    }

    private void HideNotEnoughCoinsMessage()
    {
        NotEnoughCoinsMessage.SetActive(false);
    }

    private void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
