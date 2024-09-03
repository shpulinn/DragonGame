using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsLabel;
    [SerializeField] private Button backButton;
    
    private SaveSystem saveSystem;
    private GameProgress levelProgress;

    private void OnEnable()
    {
        saveSystem = new SaveSystem();
        levelProgress = saveSystem.LoadGame();
        backButton.onClick.AddListener(CloseShop);

        coinsLabel.text = levelProgress.Coins.ToString();
    }

    private void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
