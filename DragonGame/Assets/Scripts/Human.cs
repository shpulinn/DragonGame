using UnityEngine;

public class Human : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;
    
    public void OnPlayerCollision()
    {
        GameManager.Instance.CatchHuman();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }
}
