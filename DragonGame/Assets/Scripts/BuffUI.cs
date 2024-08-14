using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    [SerializeField] private GameObject buffUIHolder;
    [SerializeField] private UIManager _uiManager;

    [Space] [SerializeField] private GameObject doubleMoneyIcon;
    [SerializeField] private GameObject diamondsIcon;
    [SerializeField] private GameObject specialIcon;
    [SerializeField] private GameObject throughHumansIcon;
    [SerializeField] private GameObject throughTrapsIcon;
    [SerializeField] private GameObject destroyIcon;


    private Dictionary<string, GameObject> buffsIconsDict;

    private void OnEnable()
    {
        GlobalEventManager.OnBuffEnabled.AddListener(ShowBuffUI);

        buffsIconsDict = new Dictionary<string, GameObject>
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
        if (!buffsIconsDict.ContainsKey(buff.buffName))
            return;
        buffsIconsDict[buff.buffName].gameObject.SetActive(true);
        
        buffUIHolder.SetActive(true);
        Invoke(nameof(HideBuffUI), buff.buffDuration);
        _uiManager.StartTimer(buff.buffDuration);
    }

    private void HideBuffUI()
    {
        HideAllIcons();
        buffUIHolder.SetActive(false);
    }

    private void HideAllIcons()
    {
        foreach (KeyValuePair<string,GameObject> o in buffsIconsDict)
        {
            o.Value.SetActive(false);
        }
    }
}