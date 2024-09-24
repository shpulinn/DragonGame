using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SecondShopScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondsCountText;
    [Space]
    [SerializeField] private Button LimitedOfferMagnetButton;
    [SerializeField] private int coinMagnetPrice = 12;
    [SerializeField] private GameObject successBuyMagnetScreen;
    [Space]
    [SerializeField] private Button LimitedOfferCoinsButton;
    [SerializeField] private int coinsPrice = 12;
    [SerializeField] private int coinsToAdd = 50;
    [SerializeField] private GameObject successBuyCoinsScreen;
    [Space]
    [SerializeField] private Button DiamondsOfferButton;
    [SerializeField] private Button NoAdsOfferButton;
    [Space] [SerializeField] private GameObject infoMessageObj;
    [SerializeField] private TextMeshProUGUI infoMessageText;

    private void Awake()
    {
        LimitedOfferMagnetButton.onClick.AddListener(LimitedOfferMagnetButtonClick);
        LimitedOfferCoinsButton.onClick.AddListener(LimitedOfferCoinsButtonClick);
        DiamondsOfferButton.onClick.AddListener(DiamondsOfferButtonClick);
        NoAdsOfferButton.onClick.AddListener(NoAdsOfferButtonClick);
    }

    private void UpdateDiamonds()
    {
        diamondsCountText.text = SaveSystem.Instance.GetCurrentProgress().Diamonds.ToString();
    }

    private void ShowInfoMessage(string message)
    {
        infoMessageText.text = message;
        infoMessageObj.SetActive(true);
    }

    private void OnEnable()
    {
        UpdateDiamonds();
    }

    private void LimitedOfferMagnetButtonClick()
    {
        if (CoinsMagnetItem.IsActive)
        {
            ShowInfoMessage("Coin magnet is active already!");
            return;
        }
        if (SaveSystem.Instance.TrySpentDiamonds(coinMagnetPrice))
        {
            CoinsMagnetItem.IsActive = true;
            UpdateDiamonds();
            successBuyMagnetScreen.SetActive(true);
        } else 
            ShowInfoMessage("Not enough diamonds!");
    }
    
    private void LimitedOfferCoinsButtonClick()
    {
        if (SaveSystem.Instance.TrySpentDiamonds(coinsPrice))
        {
            UpdateDiamonds();
            CoinsManager.Instance.AddCoins(coinsToAdd);
            successBuyCoinsScreen.SetActive(true);
        } else 
            ShowInfoMessage("Not enough diamonds!");
    }
    
    private void DiamondsOfferButtonClick()
    {
        
    }
    
    private void NoAdsOfferButtonClick()
    {
        
    }
}