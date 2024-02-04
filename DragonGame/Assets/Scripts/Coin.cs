using UnityEngine;

public class Coin : MonoBehaviour, ICollidable
{
    public void OnPlayerCollision()
    {
        GameManager.Instance.AddCoin();
        Destroy(gameObject);
    }
}
