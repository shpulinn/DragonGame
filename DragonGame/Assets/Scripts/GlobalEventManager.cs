using ScriptableObjects;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<int> OnCoinCollected = new UnityEvent<int>();
    public static UnityEvent OnHumanCollected = new UnityEvent();
    public static UnityEvent<int> OnProgressReached = new UnityEvent<int>();
    public static UnityEvent<DragonBuffs> OnBuffEnabled = new UnityEvent<DragonBuffs>();    


    public static void SendCoinCollected(int amount = 0)
    {
        OnCoinCollected.Invoke(amount);
    }

    public static void SendHumanCollected()
    {
        OnHumanCollected.Invoke();
    }

    public static void SendProgressReached(int amount = 0)
    {
        OnProgressReached.Invoke(amount);
    }

    public static void SendBuffEnabled(DragonBuffs buff)
    {
        OnBuffEnabled.Invoke(buff);
    }
}