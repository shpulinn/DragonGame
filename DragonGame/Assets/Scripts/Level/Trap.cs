using UnityEngine;

public class Trap : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip AudioClip;

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
        if (AudioClip)
        {
            _audioSource.PlayOneShot(AudioClip);
        }
        GameManager.Instance.GameOver();
    }
}
