using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<int> OnCoinCollected = new UnityEvent<int>();
    public static UnityEvent OnHumanCollected = new UnityEvent();

    public static void SendCoinCollected(int amount = 0)
    {
        OnCoinCollected.Invoke(amount);
    }

    public static void SendHumanCollected()
    {
        OnHumanCollected.Invoke();
    }
}