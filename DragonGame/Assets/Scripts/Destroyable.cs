using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Destroyable : MonoBehaviour, IDestroyable
{
    [SerializeField] private ParticleSystem destroyingParticles;
    [SerializeField] private AudioClip explosionSound;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        if (explosionSound)
        {
            _audioSource.clip = explosionSound;
        }
    }

    public void OnPlayerCollide()
    {
        // анимация разрушения объекта
        if (destroyingParticles)
        {
            Instantiate(destroyingParticles, transform.position, Quaternion.identity);
        }
        //_audioSource.Play();
        if (explosionSound)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Destroy(gameObject);
    }
}
