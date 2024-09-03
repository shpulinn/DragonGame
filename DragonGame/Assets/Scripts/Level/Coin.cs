using UnityEngine;

public class Coin : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;

    public void OnPlayerCollision()
    {
        GlobalEventManager.SendCoinCollected(1);
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        gameObject.SetActive(false);
    }
}
