using System;
using TMPro;
using UnityEngine;

public class HumansTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI humansText;
    
    private void OnEnable()
    {
        GlobalEventManager.OnHumanCollected.AddListener(UpdateText);
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (humansText)
        {
            Invoke(nameof(DelayedTextUpdate), .1f);
        }
    }

    private void DelayedTextUpdate()
    {
        humansText.text = $"{GameManager.Instance.CurrentHumanCatched.ToString()}/{GameManager.Instance.MaxHumanCatchAmount.ToString()}";
    }
}