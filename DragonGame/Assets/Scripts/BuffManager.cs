using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ScriptableObjects;

public class BuffManager : MonoBehaviour
{
    private static BuffManager _instance;
    public static BuffManager Instance => _instance;

    //private Dictionary<string, DragonBuffs> activeBuffs = new Dictionary<string, DragonBuffs>();
    private DragonBuffs activeBuff;

    private bool _isAnBuffActive = false;

    public bool IsAnyBuffActive => _isAnBuffActive;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateBuff(DragonBuffs buff)
    {
        // if (!activeBuffs.ContainsKey(buff.buffName))
        // {
        //     activeBuffs[buff.name] = buff;
        //     GlobalEventManager.SendBuffEnabled(buff.buffName, buff.buffDuration);
        //     StartCoroutine(DeactivateBuffAfterDuration(buff));
        //     Debug.Log("Buff: " + buff.buffName + " activated");
        //     _isAnBuffActive = true;
        // }
        if (_isAnBuffActive)
        {
            return;
        }
        activeBuff = buff;
        GlobalEventManager.SendBuffEnabled(buff.buffName, buff.buffDuration);
        StartCoroutine(DeactivateBuffAfterDuration(buff));
        Debug.Log("Buff: " + buff.buffName + " activated");
        _isAnBuffActive = true;
        
    }

    private IEnumerator DeactivateBuffAfterDuration(DragonBuffs buff)
    {
        yield return new WaitForSeconds(buff.buffDuration);
        DeactivateBuff(buff);
        Debug.Log("Buff: " + buff.buffName + " DEactivated");

    }

    private void DeactivateBuff(DragonBuffs buff)
    {
        if (activeBuff == buff)
        {
            // Вы можете добавить событие для деактивации баффа в GlobalEventManager, если это необходимо
            // GlobalEventManager.SendBuffDisabled(buff.buffName);
            _isAnBuffActive = false;
            activeBuff = null;
        }
    }

    public bool IsBuffActive(string buffName)
    {
        return activeBuff.buffName.Equals(buffName);
    }

    public int GetCoinMultiplier()
    {
        int multiplier = 1;
        if (activeBuff.enableCoinsMultiplier)
        {
            multiplier *= activeBuff.coinsMultiplier;
        }
        return multiplier;
    }

    // Добавьте другие методы для проверки активных баффов по мере необходимости
}