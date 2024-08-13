using System;
using TMPro;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    [SerializeField] private GameObject buffUIHolder;
    [SerializeField] private TextMeshProUGUI buffUIName;
    [SerializeField] private UIManager _uiManager;

    private void OnEnable()
    {
        GlobalEventManager.OnBuffEnabled.AddListener(ShowBuffUI);
    }

    // add icon?
    private void ShowBuffUI(string buffName, float visibilityDuration)
    {
        buffUIName.text = buffName;
        buffUIHolder.SetActive(true);
        Invoke(nameof(HideBuffUI), visibilityDuration);
        _uiManager.StartTimer(visibilityDuration);
    }

    private void HideBuffUI()
    {
        buffUIHolder.SetActive(false);
        buffUIName.text = String.Empty;
    }
}