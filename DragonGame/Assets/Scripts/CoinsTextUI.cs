using TMPro;
using UnityEngine;

public class CoinsTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    private void OnEnable()
    {
        GlobalEventManager.OnCoinCollected.AddListener(UpdateText);
    }

    private void UpdateText(int amount)
    {
        if (coinsText)
        {
            coinsText.text = CoinsManager.Instance.CurrentCoins.ToString();
        }
    }
}