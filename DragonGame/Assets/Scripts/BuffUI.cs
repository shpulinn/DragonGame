using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    [SerializeField] private GameObject buffUIHolder;
    [SerializeField] private UIManager uiManager;

    [Space] [SerializeField] private GameObject doubleMoneyIcon;
    [SerializeField] private GameObject diamondsIcon;
    [SerializeField] private GameObject specialIcon;
    [SerializeField] private GameObject throughHumansIcon;
    [SerializeField] private GameObject throughTrapsIcon;
    [SerializeField] private GameObject destroyIcon;


    private Dictionary<string, GameObject> _buffsIconsDict;

    private void OnEnable()
    {
        GlobalEventManager.OnBuffEnabled.AddListener(ShowBuffUI);

        _buffsIconsDict = new Dictionary<string, GameObject>
        {
            { "DoubleMoney", doubleMoneyIcon },
            { "Diamonds", diamondsIcon },
            { "Special", specialIcon },
            { "ThroughHumans", throughHumansIcon },
            { "ThroughTraps", throughTrapsIcon },
            { "Destroy", destroyIcon }
        };

        HideAllIcons();
    }

    private void ShowBuffUI(DragonBuffs buff)
    {
        if (!_buffsIconsDict.ContainsKey(buff.buffName))
            return;
        _buffsIconsDict[buff.buffName].gameObject.SetActive(true);
        
        buffUIHolder.SetActive(true);
        Invoke(nameof(HideBuffUI), buff.buffDuration);
        uiManager.StartTimer(buff.buffDuration);
    }

    private void HideBuffUI()
    {
        HideAllIcons();
        buffUIHolder.SetActive(false);
    }

    private void HideAllIcons()
    {
        foreach (KeyValuePair<string,GameObject> o in _buffsIconsDict)
        {
            o.Value.SetActive(false);
        }
    }
}