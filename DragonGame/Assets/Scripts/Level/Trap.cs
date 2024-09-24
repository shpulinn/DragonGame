using UnityEngine;

public class Trap : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip AudioClip;
    [SerializeField] private GameObject ExplosionParticles;

    private AudioSource _audioSource;

    private GameManager _gameManager;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _gameManager = GameManager.Instance;
    }

    public void OnPlayerCollision()
    {
        if (_gameManager.IsPaused) return;
        if (GameManager.Instance.TryTakeDamage())
        {
            if (AudioClip)
            {
                _audioSource.PlayOneShot(AudioClip);
            }

            if (ExplosionParticles)
            {
                Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
            }
        }
    }
}
