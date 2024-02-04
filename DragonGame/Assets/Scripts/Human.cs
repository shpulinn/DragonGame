using UnityEngine;

public class Human : MonoBehaviour, ICollidable
{
    public void OnPlayerCollision()
    {
        GameManager.Instance.CatchHuman();
        Destroy(gameObject);
    }
}
