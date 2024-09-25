/*using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Destroyable : MonoBehaviour, IDestroyable
{
    [SerializeField] private ParticleSystem destroyingParticles;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private int destructionLevelNeeded = 1;

    private AudioSource _audioSource;

    public int DestructionLevelNeeded => destructionLevelNeeded;
    
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
            float randomPitch = Random.Range(.8f, 1.2f);
            _audioSource.pitch = randomPitch;
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Destroy(gameObject);
    }
}*/

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Destroyable : MonoBehaviour, IDestroyable
{
    [SerializeField] private ParticleSystem destroyingParticles;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private bool enableDestroyingAnim = true;
    [Space] 
    
    [SerializeField] private int destructionLevelNeeded = 1;
    [Space]
    [SerializeField] private List<GameObject> spawnObjects; // Объекты для появления при разрушении
    [SerializeField] private float spawnChance = 0.5f; // Шанс появления объектов если destructionLevelNeeded = 0
    [SerializeField] private float spawnRadius = 2f; // Радиус разлета объектов
    [SerializeField] private float spawnForce = 5f; // Сила, с которой объекты разлетаются

    private AudioSource _audioSource;

    public int DestructionLevelNeeded => destructionLevelNeeded;
    
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
        // Анимация разрушения объекта
        if (destroyingParticles)
        {
            Instantiate(destroyingParticles, transform.position, Quaternion.identity);
        }

        // Воспроизведение звука
        if (explosionSound)
        {
            float randomPitch = Random.Range(.8f, 1.2f);
            _audioSource.pitch = randomPitch;
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // Спавн объектов
        SpawnObjects();

        if (enableDestroyingAnim)
        {
            transform.DOScale(Vector3.zero, .5f);
            Destroy(gameObject, .5f);

        } else
            Destroy(gameObject);
    }

    private void SpawnObjects()
    {
        if (spawnObjects == null || spawnObjects.Count == 0) return;

        bool shouldSpawn = destructionLevelNeeded > 0 || Random.value < spawnChance;

        if (shouldSpawn)
        {
            foreach (GameObject obj in spawnObjects)
            {
                if (obj != null)
                {
                    Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
                    Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);

                    GameObject spawnedObj = Instantiate(obj, spawnPosition, Quaternion.identity);
                    
                    Rigidbody rb = spawnedObj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 forceDirection = (spawnedObj.transform.position - transform.position).normalized;
                        rb.AddForce(forceDirection * spawnForce, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
