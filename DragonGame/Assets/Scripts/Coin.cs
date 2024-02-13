using UnityEngine;

public class Coin : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;
    
    public void OnPlayerCollision()
    {
        GameManager.Instance.AddCoin();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }
}
